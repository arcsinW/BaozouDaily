using BaoZouRiBao.Background;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    /// 后台任务注册类
    /// </summary>
    public class BackgroundTaskRegisterHelper
    {
        private const string ToastTaskName = "ToastTask";
        private static readonly string TOASTTASK_ENTRY_POINT = typeof(ToastBackgroundTask).FullName; //"BaoZouRiBao.Background.ToastBackgroundTask";

        private const string TileTaskName = "TileTask";
        private static readonly string TILETASK_ENTRY_POINT = typeof(TileBackgroundTask).FullName; // "BaoZouRiBao.Background.TileBackgroundTask";

        /// <summary>
        /// 注册所有的后台任务
        /// </summary>
        public static async void RegisterAll()
        {
            BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
            if (status == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                status == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                //如果已经注册则先取消注册
                foreach (var t in BackgroundTaskRegistration.AllTasks)
                {
                    t.Value.Unregister(true);
                }

                // 注册toastTask
                BackgroundTaskBuilder toastBuilder = new BackgroundTaskBuilder()
                {
                    Name = ToastTaskName,
                    TaskEntryPoint = TOASTTASK_ENTRY_POINT
                };

                toastBuilder.SetTrigger(new ToastNotificationActionTrigger());

                toastBuilder.Register();

                // 注册tileTask
                BackgroundTaskBuilder tileBuilder = new BackgroundTaskBuilder()
                {
                    Name = TileTaskName,
                    TaskEntryPoint = TILETASK_ENTRY_POINT
                };

                tileBuilder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
                tileBuilder.SetTrigger(new TimeTrigger(60, false));

                tileBuilder.Register();
            }
        }

        /// <summary>
        /// 注销所有后台任务
        /// </summary>
        public static void UnRegisterAll()
        {
            foreach (var item in BackgroundTaskRegistration.AllTasks)
            {
                item.Value.Unregister(true);
            }
        }
    }
}
