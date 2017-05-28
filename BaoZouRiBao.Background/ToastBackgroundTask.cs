using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.UI.Notifications;

namespace BaoZouRiBao.Background
{
    /// <summary>
    /// 在后台接收Toast通知
    /// </summary>
    public sealed class ToastBackgroundTask : IBackgroundTask
    {
        private ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var details = taskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;

            if (details != null)
            {
                LocalSettings.Values["args"] = details.Argument;
                 
                // Use the originalArgs variable to access the original arguments
                // that were passed to the app.
            }
        }
    }
}
