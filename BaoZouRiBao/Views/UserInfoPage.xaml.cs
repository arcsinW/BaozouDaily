using BaoZouRiBao.Helper;
using BaoZouRiBao.Model;
using BaoZouRiBao.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class UserInfoPage : Page
    {
        public UserInfoPage()
        {
            this.InitializeComponent();
        }

        private void TaskInfo(object sender, TappedRoutedEventArgs e)
        {
            WebViewParameter paramter = new WebViewParameter() { Title = "任务介绍", WebViewUri = "http://dailyapi.ibaozou.com/task_info",DisplayType = "3" };
            NavigationHelper.DetailFrameNavigate(typeof(WebViewPage), paramter);
        }
        
        public void CoinRank()
        {
            string uri = string.Format(Http.ServiceUri.MyCoins, GlobalValue.Current.User.AccessToken);
            WebViewParameter paramter = new WebViewParameter() { Title = "我的金币", WebViewUri = uri, DisplayType = "3" };
            NavigationHelper.DetailFrameNavigate(typeof(WebViewPage), paramter);
        }
    }
}
