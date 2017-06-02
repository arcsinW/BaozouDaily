using BaoZouRiBao.Helper;
using BaoZouRiBao.Model;
using BaoZouRiBao.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{
    public sealed partial class MainPage : Page 
    {
        public MainPage()
        {
            this.InitializeComponent(); 
        }

        private void Current_AdaptiveVisualStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            switch (e.NewState.Name)
            {
                case "Narrow":
                    splitViewButton.Visibility = Visibility.Visible;
                    pageTitleTextBlock.Margin = (Thickness)(Application.Current.Resources["NarrowPageTitleMargin"]);
                    break;
                case "Wide":
                    splitViewButton.Visibility = Visibility.Collapsed;
                    pageTitleTextBlock.Margin = (Thickness)(Application.Current.Resources["WidePageTitleMargin"]);
                    break;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            MasterDetailPage.Current.AdaptiveVisualStateChanged -= Current_AdaptiveVisualStateChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MasterDetailPage.Current.AdaptiveVisualStateChanged += Current_AdaptiveVisualStateChanged;
        }

        #region SplitView' Pane Method
        public void FavoritePage()
        {
            //splitView.IsPaneOpen = false;
            NavigationHelper.MasterFrameNavigate(typeof(MyFavoritePage));
        }

        public void MyCommentPage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(MyCommentPage));
        }

        public void ReadHistoryPage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(MyReadHistoryPage));
        }

        public void LoginPage()
        {
            if (DataShareManager.Current.User != null)
            {
                NavigationHelper.MasterFrameNavigate(typeof(UserInfoPage));
            }
            else
            {
                NavigationHelper.MasterFrameNavigate(typeof(LoginPage));
            }
        }

        public void NavigateToMainPage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(MainPage));
        }

        public void RankPage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(RankPage));
        }

        public void ChannelPage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(ChannelPage));
        }

        public void SearchPage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(SearchPage));
        }

        public void SettingPage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(SettingPage));
        }

        public void DownloadOfflinePage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(DownloadOfflinePage));
        }

        public void DayNightMode()
        {
            if (this.RequestedTheme == ElementTheme.Dark)
            {
                DataShareManager.Current.UpdateAppTheme(ElementTheme.Light);
            }
            else
            {
                DataShareManager.Current.UpdateAppTheme(ElementTheme.Dark);
            }
        }
        #endregion
         
        private void headerFlipView_Tapped(object sender, TappedRoutedEventArgs e)
        { 
            var story = headerFlipView.SelectedItem as Document;
            if (story != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = "", WebViewUri = story.Url, DocumentId = story.DocumentId, DisplayType = story.DisplayType };
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
            }
        }

        public async void Refresh()
        {
            switch (pivot.SelectedIndex)
            {
                case 0:
                    await ViewModel.RefreshDocument();
                    break;
                case 1:
                    await ViewModel.RefreshContribute();
                    break;
                case 2:
                    await ViewModel.RefreshVideo();
                    break;
            }
        }

        private void headerFlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var story = headerFlipView.SelectedItem as Document;
            if (story != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = "", WebViewUri = story.Url, DocumentId = story.DocumentId, DisplayType = story.DisplayType };
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
            }
        }
    }
}
