using BaoZouRiBao.Controls;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Interfaces;
using BaoZouRiBao.Model.ResultModel;
using BaoZouRiBao.UserControls;
using System;
using Windows.ApplicationModel;
using Windows.Networking.PushNotifications;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{
    public sealed partial class MasterDetailPage : Page, IBackKeyPressManager
    {
        public MasterDetailPage()
        {
            this.InitializeComponent();
            Current = this;
            
            SystemNavigationManager.GetForCurrentView().BackRequested += MasterDetailPage_BackRequested;
            GlobalDialog.InitializeDialog(rootGrid, this);
        }

        private void MasterDetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (isIgnore)
            {
                return;
            }

            e.Handled = true;
            NavigationHelper.GoBack();
        }

        public static MasterDetailPage Current;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DetailFrame.Navigate(typeof(DefaultPage));
            MasterFrame.Navigate(typeof(MainPage));
        }

        #region Adaptive 
        private void MasterFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateBackKeyVisibility();
        }

        private void DetailFrame_Navigated(object sender, NavigationEventArgs e)
        {
            while (DetailFrame.BackStack.Count > 1)
            {
                DetailFrame.BackStack.RemoveAt(1);
            }

            UpdateBackKeyVisibility();
        }

        private void UpdateBackKeyVisibility()
        {
            if (AdaptiveVisualState.CurrentState == Narrow)
            {
                DetailFrame.Visibility = DetailFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                DetailFrame.Visibility = Visibility.Visible;
            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = DetailFrame.CanGoBack || MasterFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void AdaptiveVisualState_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateBackKeyVisibility();

            AdaptiveVisualStateChanged?.Invoke(sender, e);
        }

        public event EventHandler<VisualStateChangedEventArgs> AdaptiveVisualStateChanged;
        #endregion

        /// <summary>
        /// 显示任务Dialog
        /// </summary>
        /// <param name="taskDoneResult"></param>
        public void ShowTaskDialog(DailyTaskDoneResult taskDoneResult)
        {
            if (taskDoneResult != null && taskDoneResult.Task != null)
            {
                //taskDialog.Show(taskDoneResult);
            }
        }

        public bool isIgnore { get; set; }

        public void UnRegisterBackKeyPress() => isIgnore = true;

        public void RegisterBackKeyPress() => isIgnore = false;
       
    }
}
