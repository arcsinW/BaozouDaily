using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

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

        private void RegisterBackgroundDownloadTask()
        {
            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();

            builder.Name = BackgroundDownloadTaskName;
            builder.SetTrigger(completionGroup.Trigger);
            builder.TaskEntryPoint = BackgroundDownloadTaskEntryPoint;

            BackgroundTaskRegistration backgroundDownloadTask = builder.Register(); 
        }

        private async Task CreateDownloadFolder(string folderName)
        {
            downloadFolder = await Package.Current.InstalledLocation.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
        }

        private async void DownloadFile(Uri sourceUri, string destFileName)
        {
            var cts = new CancellationTokenSource();
            StorageFile destinationFile = await downloadFolder.CreateFileAsync(destFileName, CreationCollisionOption.GenerateUniqueName);
            DownloadOperation download = downloader.CreateDownload(sourceUri, destinationFile);

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
    }
}
