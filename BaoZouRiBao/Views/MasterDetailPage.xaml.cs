using BaoZouRiBao.Helper;
using BaoZouRiBao.Model.ResultModel;
using BaoZouRiBao.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{
    public sealed partial class MasterDetailPage : Page , INotifyPropertyChanged
    {
        public static MasterDetailPage Current;
        public MasterDetailPage()
        {
            this.InitializeComponent();
            Current = this;

            AppTheme = GlobalValue.Current.AppTheme;
            User = GlobalValue.Current.User;

            GlobalValue.Current.DataChanged += Current_DataChanged;
            StatusBarHelper.ShowStatusBar();
            SystemNavigationManager.GetForCurrentView().BackRequested += MasterDetailPage_BackRequested;
            //if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            //{
            //    Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            //}

            if(DesignMode.DesignModeEnabled)
            {
                MasterFrame.Navigate(typeof(MainPage));
                DetailFrame.Navigate(typeof(DefaultPage));
            }
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
            if (DetailFrame.CanGoBack)
            {
                DetailFrame.GoBack();
            }
            else if (MasterFrame.CanGoBack)
            {
                MasterFrame.GoBack();
            }
        }

        private void Current_DataChanged()
        {
            AppTheme = GlobalValue.Current.AppTheme;
            User = GlobalValue.Current.User;
        }

        private void MasterDetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            if(DetailFrame.CanGoBack)
            {
                DetailFrame.GoBack();
            }
            else if(MasterFrame.CanGoBack)
            {
                MasterFrame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DetailFrame.Navigate(typeof(DefaultPage));
            MasterFrame.Navigate(typeof(MainPage));
        }

        private void MasterFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateBackKeyVisibility();
        }

        private void DetailFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateBackKeyVisibility();
        }

        private void UpdateBackKeyVisibility()
        {
            if(AdaptiveVisualState.CurrentState == Narrow)
            {
                DetailFrame.Visibility = DetailFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                DetailFrame.Visibility = Visibility.Visible;
            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = DetailFrame.CanGoBack || MasterFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalValue.Current.User == null)
            {
                MasterFrame.Navigate(typeof(LoginPage));
            }
            else
            {
                MasterFrame.Navigate(typeof(UserInfoPage));
            }
        }

        private void AdaptiveVisualState_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateBackKeyVisibility();
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        
        #region Pane Method
        public void LoginPage()
        {
            if (GlobalValue.Current.User != null)
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
            if(AppTheme == ElementTheme.Dark)
            {
                GlobalValue.Current.UpdateAppTheme(ElementTheme.Light);
            }
            else
            {
                GlobalValue.Current.UpdateAppTheme(ElementTheme.Dark);
            }
        }
        #endregion

        public void ShowTaskPopup(TaskDoneResult taskDoneResult)
        {
            taskPopup.BaoZouPopupType = taskDoneResult.Task.TaskType;
            taskPopup.CoinCount = taskDoneResult.Task.Amount;
            taskPopup.Visibility = Visibility.Visible;
        }
    }
}
