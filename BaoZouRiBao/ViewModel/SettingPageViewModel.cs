using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Model;
using Windows.Storage;
using BaoZouRiBao.Http;
using Windows.Foundation.Collections;
using BaoZouRiBao.Model.ResultModel;
using BaoZouRiBao.UserControls;

namespace BaoZouRiBao.ViewModel
{
    public class SettingPageViewModel : ViewModelBase
    {
        public SettingPageViewModel()
        {
            LoadSettings();

            GlobalValue.Current.DataChanged += Current_DataChanged;

            if (GlobalValue.Current.User != null)
            {
                IsLogoutEnable = true;
            }
        }

        private void Current_DataChanged()
        {
            if (GlobalValue.Current.User != null)
            {
                IsLogoutEnable = true;
            }
        }

        private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        #region Keys
        private const string IsAutoCache_Key = "IsAutoCache";
        private const string IsSmallImage_Key = "IsSmallImage";
        private const string IsBigFont_Key = "IsBigFont";
        private const string IsNewsNotify_Key = "IsNewsNotify";
        #endregion

        #region Properties

        private bool isAutoCache = false;

        /// <summary>
        /// 是否自动离线下载
        /// </summary>
        public bool IsAutoCache
        {
            get
            {
                return isAutoCache;
            }

            set
            {
                localSettings.Values[IsAutoCache_Key] = value;
            }
        }

        private bool isSmallImage = false;

        /// <summary>
        /// 是否缩略图模式
        /// </summary>
        public bool IsSmallImage
        {
            get
            {
                return isSmallImage;
            }

            set
            {
                localSettings.Values[IsSmallImage_Key] = value;
            }
        }

        private bool isBigFont = false;

        /// <summary>
        /// 是否文章大字号
        /// </summary>
        public bool IsBigFont
        {
            get
            {
                return isBigFont;
            }

            set
            {
                localSettings.Values[IsBigFont_Key] = value;
            }
        }

        private bool isNewsNotify = false;

        /// <summary>
        /// 是否新消息通知
        /// </summary>
        public bool IsNewsNotify
        {
            get
            {
                return isNewsNotify;
            }

            set
            {
                localSettings.Values[IsNewsNotify_Key] = value;
            }
        }


        private bool isLogoutEnable;

        public bool IsLogoutEnable
        {
            get { return isLogoutEnable; }
            set { isLogoutEnable = value; OnPropertyChanged(); }
        }

        public string AppVersion { get; set; } = $"{InformationHelper.ApplicationVersion.Major}.{InformationHelper.ApplicationVersion.Minor}.{InformationHelper.ApplicationVersion.Build}.{InformationHelper.ApplicationVersion.Revision}";
        #endregion
         
        public void LoadSettings()
        {
            IsAutoCache = (bool)GetValue(localSettings, false, IsAutoCache_Key);
            IsSmallImage = (bool)GetValue(localSettings,false,IsSmallImage_Key);
            isBigFont = (bool)GetValue(localSettings,false,IsBigFont_Key);
            IsNewsNotify = (bool)GetValue(localSettings, false, IsNewsNotify_Key);
        }
        
        private object GetValue(ApplicationDataContainer set,object defaultValue,string key)
        {
            if(set.Values.ContainsKey(key))
            {
                return set.Values[key];
            }
            return defaultValue;
        }

        public async void Logout()
        {
            bool result = await ApiService.Instance.LogoutAsync();
            if (result)
            {
                ToastService.SendToast("已注销登录");
            }
        }
    }
}
