using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Networking.BackgroundTransfer;

namespace BaoZouRiBao.Background
{
    /// <summary>
    /// 后台下载的后台任务
    /// </summary>
    public sealed class BackgroundDownloadTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            var detail = (BackgroundTransferCompletionGroupTriggerDetails)taskInstance.TriggerDetails;

            if (detail == null)
            {
                return;
            }

            IReadOnlyList<DownloadOperation> downloads = detail.Downloads;

            foreach (DownloadOperation item in downloads)
            {

            }

            // post-processing

            deferral.Complete();
        }

        /// <summary>
        /// 下载是否失败
        /// </summary>
        /// <param name="download"></param>
        /// <returns></returns>
        private bool IsFailed(DownloadOperation download)
        {
            BackgroundTransferStatus status = download.Progress.Status;
            if (status == BackgroundTransferStatus.Error || status == BackgroundTransferStatus.Canceled)
            {
                return true;
            }

            ResponseInformation response = download.GetResponseInformation();
            if (response.StatusCode != 200)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 重试下载
        /// </summary>
        /// <param name="downloads"></param>
        private void RetryDownloads(IEnumerable<DownloadOperation> downloads)
        {
            BackgroundDownloader downloader = BackgroundDownloadTask.CreateBackgroundDownloader();

            foreach (DownloadOperation download in downloads)
            {
                DownloadOperation download1 = downloader.CreateDownload(download.RequestedUri, download.ResultFile);
                Task<DownloadOperation> startTask = download1.StartAsync().AsTask();
            }

            downloader.CompletionGroup.Enable();
        }

        //public void InvokeSimpleToast(int succeeded, int failed)
        //{
        //    XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);

        //    XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
        //    stringElements.Item(0).AppendChild(toastXml.CreateTextNode(String.Format(
        //        CultureInfo.InvariantCulture,
        //        "{0} downloads succeeded.",
        //        succeeded)));
        //    stringElements.Item(1).AppendChild(toastXml.CreateTextNode(String.Format(
        //        CultureInfo.InvariantCulture,
        //        "{0} downloads failed.",
        //        failed)));

        //    ToastNotification toast = new ToastNotification(toastXml);
        //    ToastNotificationManager.CreateToastNotifier().Show(toast);
        //}

        public static BackgroundDownloader CreateBackgroundDownloader()
        {
            BackgroundTransferCompletionGroup completionGroup = new BackgroundTransferCompletionGroup();

            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
            builder.TaskEntryPoint = "Tasks.CompletionGroupTask";
            builder.SetTrigger(completionGroup.Trigger);

            BackgroundTaskRegistration taskRegistration = builder.Register();

            BackgroundDownloader downloader = new BackgroundDownloader(completionGroup);

            return downloader;
        }
    }
}
