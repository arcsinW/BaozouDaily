using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources.Core;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.ViewManagement;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    /// 获取一些设备信息
    /// </summary>
    public class InformationHelper
    {
        static InformationHelper()
        {
            EasClientDeviceInformation easDeviceInfo = new EasClientDeviceInformation();

            ApplicationVersion = Package.Current.Id.Version.ToString();
            DeviceId = easDeviceInfo.Id.ToString();

            ResourceContext resContext = ResourceContext.GetForCurrentView();
            string value = resContext.QualifierValues["DeviceFamily"];
            IsMobile = value.Equals("Mobile");

            ProductName = easDeviceInfo.SystemProductName;
            HardwareVersion = easDeviceInfo.SystemHardwareVersion;

            ScreenWidth = ApplicationView.GetForCurrentView().VisibleBounds.Width;
            ScreenHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height;
        }

        /// <summary>
        /// Gets Application's version
        /// </summary>
        public static string ApplicationVersion { get; private set; }

        /// <summary>
        /// Gets device id
        /// </summary>
        public static string DeviceId { get; private set; }

        /// <summary>
        /// Gets product name
        /// </summary>
        public static string ProductName { get; private set; }

        /// <summary>
        /// Gets hardware version
        /// </summary>
        public static string HardwareVersion { get; private set; }

        /// <summary>
        /// Gets screen's height
        /// </summary>
        public static double ScreenHeight { get; private set; }

        /// <summary>
        /// Gets screen's width
        /// </summary>
        public static double ScreenWidth { get; private set; }

        /// <summary>
        /// Gets a value indicating whether if this is mobile
        /// </summary>
        public static bool IsMobile { get; private set; }
    }
}
