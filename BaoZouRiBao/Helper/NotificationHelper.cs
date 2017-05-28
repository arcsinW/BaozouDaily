using System;
using Microsoft.Services.Store.Engagement;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    /// 用于接受商店发送的推送
    /// </summary>
    public class NotificationHelper
    {
        public static async void Register()
        {
            StoreServicesEngagementManager engagementManager = StoreServicesEngagementManager.GetDefault();
            await engagementManager.RegisterNotificationChannelAsync();
        }

        public static async void UnRegister()
        {
            StoreServicesEngagementManager engagementManager = StoreServicesEngagementManager.GetDefault();
            await engagementManager.UnregisterNotificationChannelAsync();
        }
    }
}
