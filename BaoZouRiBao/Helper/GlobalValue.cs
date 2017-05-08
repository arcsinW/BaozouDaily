using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using BaoZouRiBao.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    /// Store all of the settings and user's data
    /// </summary>
    public class GlobalValue
    {
        #region Singleton
        
        private static GlobalValue current = new GlobalValue();
        public static GlobalValue Current
        {
            get
            {
                return current;
            }
        }

        private GlobalValue()
        {
            provider = new PersistentProvider(StorageType.Local);
            LoadData();
            
        }
        #endregion

        private PersistentProvider provider;


#if DEBUG
        public const string AccessKey = "";
        public const string SecretKey = "";
#else
        private const string AccessKey = "e132054cb486408fa2add8972b1c4cc3";
        private const string SecretKey = "425de3dbe3874a038c2a07816af215de";
#endif
         
        #region Keys
        private const string Settings_Key = "Settings";
        private const string User_Key = "User";
        private const string AppTheme_Key = "AppTheme";
        #endregion

        #region Properties

        private User user;

        public User User
        {
            get { return user; }
            set
            {
                user = value;
                provider.UpdateItem(User_Key, user);
            }
        }

        private ElementTheme appTheme = ElementTheme.Light;
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }
            set
            {
                appTheme = value;
                provider.UpdateItem(AppTheme_Key, appTheme);
            }
        }

        /// <summary>
        /// 是否当日首次打开 用于完成每日签到任务
        /// </summary>
        public bool IsToDayFirstStart { get; set; } = false;
        
        private Settings appSettings;
        public Settings AppSettings
        {
            get { return appSettings; }
            set
            {
                appSettings = value;
                provider.UpdateItem(Settings_Key, appSettings);
            }
        }

        #endregion

        /// <summary>
        /// 切换日夜间模式
        /// </summary>
        public void SwitchElementTheme()
        {
            if (AppTheme == ElementTheme.Dark)
            {
                Current.UpdateAppTheme(ElementTheme.Light);
                StatusBarHelper.ShowStatusBar(false);
            }
            else
            {
                Current.UpdateAppTheme(ElementTheme.Dark);
                StatusBarHelper.ShowStatusBar(true);
            }
        }

        private void LoadData()
        {
            appSettings = provider.GetItem<Settings>(Settings_Key);
            if (appSettings == null)
            {
                appSettings = new Settings();
            }

            user = provider.GetItem<User>(User_Key);
            if (user == null)
            {
                user = new User();
            }

            AppTheme = provider.GetItem<ElementTheme>(AppTheme_Key);
        }

        #region Update methods

        public void UpdateUser(User user)
        {
            User = user; 
            OnDataChanged();
        }
          
        public void UpdateAppTheme(ElementTheme theme)
        {
            AppTheme = theme;
            OnDataChanged();
            ThemeChanged?.Invoke(null, theme == ElementTheme.Light);
        }
        #endregion

        #region Data changed 
        public delegate void DataChangedEventHandler();

        public event DataChangedEventHandler DataChanged;

        /// <summary>
        /// Light Dark 主题变化  
        /// true 表示变成 Light
        /// false 表示变成 Dark
        /// </summary>
        public event EventHandler<bool> ThemeChanged;

        private void OnDataChanged()
        {
            DataChanged?.Invoke();
        }
        #endregion
    }
}
