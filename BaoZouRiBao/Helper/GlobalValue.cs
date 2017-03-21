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
        public const string SECRET_KEY = "18a75cf12dff8cf6e17550e25c860839";

        #region Properties
        
        public User User { get; set; }
         
        public string AccessToken { get; set; }
       
        public ElementTheme AppTheme { get; set; }


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
        /// Load global value
        /// </summary>

        private void LoadData()
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
