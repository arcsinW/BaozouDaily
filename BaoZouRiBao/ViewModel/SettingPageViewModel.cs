using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Model;
using Windows.Storage;

namespace BaoZouRiBao.ViewModel
{
    public class SettingPageViewModel : ViewModelBase
    {
        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        #region Keys
        private const string IsAutoCache_Key = "IsAutoCache";
        private const string IsSmallImage_Key = "IsSmallImage";
        private const string IsBigFont_Key = "IsBigFont";
        private const string IsNewsNotify_Key = "IsNewsNotify";
        #endregion

        #region Properties

        /// <summary>
        /// 是否自动离线下载
        /// </summary>
        public bool IsAutoCache
        {
            get
            {
                return (bool)localSettings.Values[IsAutoCache_Key];
            }

            set
            {
                localSettings.Values[IsAutoCache_Key] = value;
            }
        }

        /// <summary>
        /// 是否缩略图模式
        /// </summary>
        public bool IsSmallImage
        {
            get
            {
                return (bool)localSettings.Values[IsSmallImage_Key];
            }

            set
            {
                localSettings.Values[IsSmallImage_Key] = value;
            }
        }

        /// <summary>
        /// 是否文章大字号
        /// </summary>
        public bool IsBigFont
        {
            get
            {
                return (bool)localSettings.Values[IsBigFont_Key];
            }

            set
            {
                localSettings.Values[IsBigFont_Key] = value;
            }
        }

        /// <summary>
        /// 是否新消息通知
        /// </summary>
        public bool IsNewsNotify
        {
            get
            {
                return (bool)localSettings.Values[IsNewsNotify_Key];
            }

            set
            {
                localSettings.Values[IsNewsNotify_Key] = value;
            }
        }

        public string AppVersion { get; set; } = InformationHelper.ApplicationVersion;
        #endregion
    }
}
