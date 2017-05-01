using BaoZouRiBao.Helper;
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

        public MasterDetailPageViewModel()
        {
            AppTheme = GlobalValue.Current.AppTheme;
            User = GlobalValue.Current.User;

            GlobalValue.Current.DataChanged += Current_DataChanged;
            StatusBarHelper.ShowStatusBar(appTheme == ElementTheme.Dark);
            SystemNavigationManager.GetForCurrentView().BackRequested += MasterDetailPage_BackRequested;
        }

        private void Current_DataChanged()
        {
            AppTheme = GlobalValue.Current.AppTheme;
            User = GlobalValue.Current.User;
        }

        private void MasterDetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            NavigationHelper.GoBack();
        }

        #region Pane Method
        public void LoginPage()
        {
            if (GlobalValue.Current.User != null && !string.IsNullOrEmpty(GlobalValue.Current.User.AccessToken))
            {
                NavigationHelper.MasterFrameNavigate(typeof(UserInfoPage));
            }
            else
            {
                NavigationHelper.MasterFrameNavigate(typeof(LoginPage));
            }
        }

        public void DayNightMode()
        {
            if (AppTheme == ElementTheme.Dark)
            {
                GlobalValue.Current.UpdateAppTheme(ElementTheme.Light);
                StatusBarHelper.ShowStatusBar(false);
            }
            else
            {
                GlobalValue.Current.UpdateAppTheme(ElementTheme.Dark);
                StatusBarHelper.ShowStatusBar(true);
            }
        }
        #endregion
    }
}
