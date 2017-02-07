using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.ViewManagement;

namespace BaoZouRiBao.Core.Helper
{
    /// <summary>
    /// 获取一些设备信息
    /// </summary>
    public class DeviceInformationHelper
    {
        private static EasClientDeviceInformation easDeviceInfo = new EasClientDeviceInformation();
        /// <summary>
        /// return local device id
        /// </summary>
        /// <returns></returns>
        public static string GetDeviceId()
        {
            return easDeviceInfo.Id.ToString();
        }

        /// <summary>
        /// product name
        /// </summary>
        /// <returns></returns>
        public static string GetProductName()
        {
            return easDeviceInfo.SystemProductName;
        }

        /// <summary>
        /// OS
        /// </summary>
        /// <returns></returns>
        public static string GetOS()
        {
            return easDeviceInfo.OperatingSystem;
        }

        /// <summary>
        /// os version
        /// </summary>
        /// <returns></returns>
        public static string GetOSVer()
        {
            return easDeviceInfo.SystemFirmwareVersion;
        }

        /// <summary>
        /// hardware version
        /// </summary>
        /// <returns></returns>
        public static string GetHardVersion()
        {
            return easDeviceInfo.SystemHardwareVersion;
        }

        /// <summary>
        /// get screen's height
        /// </summary>
        /// <returns></returns>
        public static double GetScreenHeight()
        {
            return ApplicationView.GetForCurrentView().VisibleBounds.Height;
        }

        /// <summary>
        /// get screen's width
        /// </summary>
        /// <returns></returns>
        public static double GetScreenWidth()
        {
            return ApplicationView.GetForCurrentView().VisibleBounds.Width;
        }

        /// <summary>
        /// check if it is mobile
        /// </summary>
        /// <returns></returns>
        public static bool IsMobile()
        {
            ResourceContext resContext = ResourceContext.GetForCurrentView();
            string value = resContext.QualifierValues["DeviceFamily"];
            return value.Equals("Mobile");
        }
    }
}
