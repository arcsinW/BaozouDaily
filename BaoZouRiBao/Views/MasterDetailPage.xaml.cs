using BaoZouRiBao.Model.ResultModel;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{
    public sealed partial class MasterDetailPage : Page
    {
        public static MasterDetailPage Current;
        public MasterDetailPage()
        {
            this.InitializeComponent();

            Current = this;

            if(DesignMode.DesignModeEnabled)
            {
                MasterFrame.Navigate(typeof(MainPage));
                DetailFrame.Navigate(typeof(DefaultPage));
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
        
        private void AdaptiveVisualState_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateBackKeyVisibility();
        }
          
        /// <summary>
        /// 显示任务Popup
        /// </summary>
        /// <param name="taskDoneResult"></param>
        public void ShowTaskPopup(DailyTaskDoneResult taskDoneResult)
        {
            taskPopup.BaoZouPopupType = taskDoneResult.Task.TaskType;
            taskPopup.CoinCount = taskDoneResult.Task.Amount;
            taskPopup.Visibility = Visibility.Visible;
        }
    }
}
