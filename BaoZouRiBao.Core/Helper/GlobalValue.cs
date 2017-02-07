using BaoZouRiBao.Core.Model;
using BaoZouRiBao.Core.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoZouRiBao.Core.Helper
{
    /// <summary>
    /// Store all of the settings and user's data
    /// </summary>
    public class GlobalValue
    {
        public const string SECRET_KEY = "18a75cf12dff8cf6e17550e25c860839";

        #region Properties
        private User user = null;
        public User User
        {
            get
            {
                return user;
            } 
            private set
            {
                user = value;
            }
        }

        private string accessToken;
        public string AccessToken
        {
            get
            {
                return accessToken;
            } 
            private set
            {
                accessToken = value;
            }
        }

        private ElementTheme appTheme = ElementTheme.Light;
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            } 
            private set
            {
                appTheme = value;
            }
        }
        #endregion

        #region Singleton

        private static GlobalValue current;
        public static GlobalValue Current
        {
            get
            {
                if(current==null)
                {
                    return current = new GlobalValue();
                }
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
