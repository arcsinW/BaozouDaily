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

            IReadOnlyList<DownloadOperation> downloads = detail.Downloads;

            // post-processing

            deferral.Complete();
        }
    }
}
