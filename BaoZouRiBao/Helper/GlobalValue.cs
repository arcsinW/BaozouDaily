using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    /// Store all of the settings and user's data
    /// </summary>
    public class GlobalValue
    {
        public const string AccessKey = "e132054cb486408fa2add8972b1c4cc3";
        public const string SecretKey = "425de3dbe3874a038c2a07816af215de";


        #region Properties

        public User User { get; set; }
         
        public string AccessToken { get; set; }
       
        public ElementTheme AppTheme { get; set; }

        /// <summary>
        /// 是否当日首次打开 用于完成每日签到任务
        /// </summary>
        public bool IsToDayFirstStart { get; set; }
        #endregion

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
            LoadData();
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
            }
            else
            {
                Current.UpdateAppTheme(ElementTheme.Dark);
            }
        }

        /// <summary>
        /// Load global value
        /// </summary>

        private void LoadData()
        {

        }

        /// <summary>
        /// 本地化保存
        /// </summary>
        private void SaveData()
        {

        }

        #region Update methods
        public void UpdateUser(User user)
        {
            if (user != null)
            {
                User = user;
                OnDataChanged();
            }
        }

        public void UpdateAccessToken(string accessToken)
        {
            if(!string.IsNullOrEmpty(accessToken))
            {
                AccessToken = accessToken;
                OnDataChanged();
            }
        }

        public void UpdateAppTheme(ElementTheme theme)
        {
            AppTheme = theme;
            OnDataChanged();
        }
        #endregion

        #region Data changed 
        public delegate void DataChangedEventHandler();

        public event DataChangedEventHandler DataChanged;

        private void OnDataChanged()
        {
            DataChanged?.Invoke();
        }
        #endregion
    }
}
