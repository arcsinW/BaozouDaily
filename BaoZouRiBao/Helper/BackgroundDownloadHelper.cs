using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Web;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    /// 后台下载
    /// </summary>
    public class BackgroundDownloadHelper
    {
        private const string BackgroundDownloadTaskName = "BackgroundDownload";
        private const string BackgroundDownloadTaskEntryPoint = "BaoZouRiBao.Background.BackgroundDownloadTask";

        private BackgroundDownloader downloader;
        private StorageFolder downloadFolder;
        private BackgroundTransferCompletionGroup completionGroup;

        /// <summary>
        /// 当前下载
        /// </summary>
        private List<DownloadOperation> activeDownloads;
        private CancellationTokenSource cts = new CancellationTokenSource();

        public BackgroundDownloadHelper()
        {
            completionGroup = new BackgroundTransferCompletionGroup();
            downloader = new BackgroundDownloader(completionGroup);
        }

        public async void CreateDownload(Uri uri, string destFileName)
        {
            StorageFile destinationFile = await downloadFolder.CreateFileAsync(destFileName, CreationCollisionOption.GenerateUniqueName);
            DownloadOperation download = downloader.CreateDownload(uri, destinationFile);
            Task<DownloadOperation> startTask = download.StartAsync().AsTask();

            //startTask.ContinueWith(ForegroundCompletionHandler);

            downloader.CompletionGroup.Enable();
        }

        /// <summary>
        /// 注册后台下载任务
        /// </summary>
        private void RegisterBackgroundDownloadTask()
        {
            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();

            builder.Name = BackgroundDownloadTaskName;
            builder.SetTrigger(completionGroup.Trigger);
            builder.TaskEntryPoint = BackgroundDownloadTaskEntryPoint;

            BackgroundTaskRegistration backgroundDownloadTask = builder.Register(); 
        }

        /// <summary>
        /// 创建下载目的文件夹
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        private async Task CreateDownloadFolder(string folderName)
        {
            downloadFolder = await Package.Current.InstalledLocation.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
        }

        private async void DownloadFile(Uri sourceUri, string destFileName)
        {
            var cts = new CancellationTokenSource();
            StorageFile destinationFile = await downloadFolder.CreateFileAsync(destFileName, CreationCollisionOption.GenerateUniqueName);
            DownloadOperation download = downloader.CreateDownload(sourceUri, destinationFile);

            activeDownloads.Add(download);

            Progress<DownloadOperation> progressCallback = new Progress<DownloadOperation>(DownloadProgress);
            
            try
            {
            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 此线程在后台线程上执行
        /// </summary>
        /// <param name="download"></param>
        private void DownloadProgress(DownloadOperation download)
        {
            // DownloadOperation.Progress is updated in real-time while the operation is ongoing. Therefore,
            // we must make a local copy so that we can have a consistent view of that ever-changing state
            // throughout this method's lifetime.
            BackgroundDownloadProgress currentProgress = download.Progress;
            
            double percent = 100;
            if (currentProgress.TotalBytesToReceive > 0)
            {
                percent = currentProgress.BytesReceived * 100 / currentProgress.TotalBytesToReceive;
            }

            LogHelper.WriteLine(String.Format(
                CultureInfo.CurrentCulture,
                " - Transfered bytes: {0} of {1}, {2}%",
                currentProgress.BytesReceived,
                currentProgress.TotalBytesToReceive,
                percent));

            if (currentProgress.HasRestarted)
            {
                LogHelper.WriteLine(" - Download restarted");
            }

            if (currentProgress.HasResponseChanged)
            {
                // We have received new response headers from the server.
                // Be aware that GetResponseInformation() returns null for non-HTTP transfers (e.g., FTP).
                ResponseInformation response = download.GetResponseInformation();
                int headersCount = response != null ? response.Headers.Count : 0;

                LogHelper.WriteLine(" - Response updated; Header count: " + headersCount);

                // If you want to stream the response data this is a good time to start.
                // download.GetResultStreamAt(0);
            }
        }


        private async Task HandleDownloadAsync(DownloadOperation download, bool start)
        {
            try
            {
                // Store the download so we can pause/resume.
                activeDownloads.Add(download);

                Progress<DownloadOperation> progressCallback = new Progress<DownloadOperation>(DownloadProgress);
                if (start)
                {
                    // Start the download and attach a progress handler.
                    await download.StartAsync().AsTask(cts.Token, progressCallback);
                }
                else
                {
                    // The download was already running when the application started, re-attach the progress handler.
                    await download.AttachAsync().AsTask(cts.Token, progressCallback);
                }

                ResponseInformation response = download.GetResponseInformation();

                // GetResponseInformation() returns null for non-HTTP transfers (e.g., FTP).
                string statusCode = response != null ? response.StatusCode.ToString() : String.Empty;

                LogHelper.WriteLine(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        "Completed: {0}, Status Code: {1}",
                        download.Guid,
                        statusCode));
            }
            catch (TaskCanceledException)
            {
                LogHelper.WriteLine("Canceled: " + download.Guid);
            }
            catch (Exception ex)
            {
                if (!IsExceptionHandled("Execution error", ex, download))
                {
                    throw;
                }
            }
            finally
            {
                activeDownloads.Remove(download);
            }
        }

        private bool IsExceptionHandled(string title, Exception ex, DownloadOperation download = null)
        {
            WebErrorStatus error = BackgroundTransferError.GetStatus(ex.HResult);
            if (error == WebErrorStatus.Unknown)
            {
                return false;
            }

            if (download == null)
            {
                LogHelper.WriteLine(string.Format(CultureInfo.CurrentCulture, "Error: {0}: {1}", title, error));
            }
            else
            {
                LogHelper.WriteLine(string.Format(CultureInfo.CurrentCulture, "Error: {0} - {1}: {2}", download.Guid, title,error));
            }

            return true;
        }
    }
}
