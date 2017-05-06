using BaoZouRiBao.Model;
using BaoZouRiBao.Helper;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{
    public sealed partial class MyRecommendPage : Page
    {
        public MyRecommendPage()
        {
            this.InitializeComponent();
        }
          
        private void faqBtn_Click(object sender, RoutedEventArgs e)
        {
            WebViewParameter paramter = new WebViewParameter() { Title = "如何复制", WebViewUri = "http://dailyapi.ibaozou.com/faq?category_id=1", DisplayType = "3" };
            NavigationHelper.DetailFrameNavigate(typeof(WebViewPage), paramter);
        } 
    }
}
