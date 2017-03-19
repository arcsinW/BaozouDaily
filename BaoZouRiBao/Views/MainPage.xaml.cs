using BaoZouRiBao.Helper;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using BaoZouRiBao.Helper;
using BaoZouRiBao.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace BaoZouRiBao.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page , INotifyPropertyChanged
    {
        public MainPage()
        {
            this.InitializeComponent();
            AppTheme = GlobalValue.Current.AppTheme;
            GlobalValue.Current.DataChanged += Current_DataChanged;
        }

        private void Current_DataChanged()
        {
            AppTheme = GlobalValue.Current.AppTheme;
             
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

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void documentListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var story = e.ClickedItem as Document;
            if (story != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = "", WebViewUri = story.Url, DocumentId = story.DocumentId, DisplayType = story.DisplayType };
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
            }
        }

        private void headerFlipView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var story = headerFlipView.SelectedItem as Document;
            if (story != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = "", WebViewUri = story.Url, DocumentId = story.DocumentId,DisplayType = story.DisplayType };
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
            }
        }

        private void splitViewBtn_Click(object sender, RoutedEventArgs e)
        {
            //splitView.IsPaneOpen = !splitView.IsPaneOpen;
            MasterDetailPage.Current.drawer.IsDrawerOpened = !MasterDetailPage.Current.drawer.IsDrawerOpened;
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
            if (GlobalValue.Current.User != null)
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
                GlobalValue.Current.UpdateAppTheme(ElementTheme.Light);
            }
            else
            {
                GlobalValue.Current.UpdateAppTheme(ElementTheme.Dark);
            }
        }
        #endregion

        private void contributeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var contribute = e.ClickedItem as Contribute;
            if (contribute != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = contribute.Title, DocumentId = contribute.DocumentId, DisplayType = contribute.DisplayType, WebViewUri = contribute.Url };
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
            }
        }
         
        private void videoListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var video = e.ClickedItem as Video;
            NavigationHelper.DetailFrameNavigate(typeof(VideoPage),video);
        }

        public void Refresh()
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    ViewModel.RefreshDocument();
                    break;
                case 1:
                    ViewModel.RefreshContribute();
                    break;
                case 2:
                    ViewModel.RefreshVideo();
                    break;
            }
        }

        private void PullProgressChanged(object sender, RefreshProgressEventArgs e)
        {
            if (e.IsRefreshable)
            {
                if (e.PullProgress == 1)
                {
                    // Progress = 1.0 means that the refresh has been triggered.
                    if (SpinnerStoryboard.GetCurrentState() == Windows.UI.Xaml.Media.Animation.ClockState.Stopped)
                    {
                        SpinnerStoryboard.Begin();
                    }
                }
                else if (SpinnerStoryboard.GetCurrentState() != Windows.UI.Xaml.Media.Animation.ClockState.Stopped)
                {
                    SpinnerStoryboard.Stop();
                }
                else
                {
                    // Turn the indicator by an amount proportional to the pull progress.
                    contributeSpinnerTransform.Angle = e.PullProgress * 360;
                }
            }
        }

      
    }
}
