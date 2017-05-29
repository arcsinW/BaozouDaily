using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.Model.ResultModel;
using BaoZouRiBao.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace BaoZouRiBao.ViewModel
{
    public class MasterDetailPageViewModel : ViewModelBase
    {
        public MasterDetailPageViewModel()
        {
            AppTheme = DataShareManager.Current.AppTheme;
            User = DataShareManager.Current.User;

            DataShareManager.Current.DataChanged += Current_DataChanged;
            StatusBarHelper.ShowStatusBar(AppTheme == ElementTheme.Dark);
            SystemNavigationManager.GetForCurrentView().BackRequested += MasterDetailPage_BackRequested;
        }

        #region Properties
        private User user;

        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private ElementTheme appTheme;

        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }

            set
            {
                appTheme = value;
                OnPropertyChanged();
            }
        }
        #endregion
        
        private void Current_DataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
            User = DataShareManager.Current.User;
        }

        private void MasterDetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            NavigationHelper.GoBack();
        }

        #region Pane Method
        public void LoginPage()
        {
            if (DataShareManager.Current.User != null && !string.IsNullOrEmpty(DataShareManager.Current.User.AccessToken))
            {
                NavigationHelper.MasterFrameNavigate(typeof(UserInfoPage));
            }
            else
            {
                NavigationHelper.MasterFrameNavigate(typeof(LoginPage));
            }
        }


        /// <summary>
        /// 切换日夜间模式
        /// </summary>
        public void DayNightMode()
        {
            if (AppTheme == ElementTheme.Dark)
            {
                DataShareManager.Current.UpdateAppTheme(ElementTheme.Light);
                StatusBarHelper.ShowStatusBar(false);
            }
            else
            {
                DataShareManager.Current.UpdateAppTheme(ElementTheme.Dark);
                StatusBarHelper.ShowStatusBar(true);
            }
        }

        /// <summary>
        /// 每日签到
        /// </summary>
        public void DailySign()
        {
            BaoZouTaskManager.DailySign();
        }
        #endregion
    }
}
