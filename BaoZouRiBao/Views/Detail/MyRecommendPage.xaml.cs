using BaoZouRiBao.Model;
using BaoZouRiBao.Helper;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace BaoZouRiBao.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MyRecommendPage : Page, INotifyPropertyChanged
    {
        public MyRecommendPage()
        {
            this.InitializeComponent();
            Clipboard.ContentChanged += Clipboard_ContentChanged;
        }

        private async void Clipboard_ContentChanged(object sender, object e)
        {
            DataPackageView dataPackage = Clipboard.GetContent();
            if (dataPackage.Contains(StandardDataFormats.WebLink))
            {
                string link = await dataPackage.GetTextAsync();
            }
        }

        private void faqBtn_Click(object sender, RoutedEventArgs e)
        {
            WebViewParameter paramter = new WebViewParameter() { Title = "如何复制", WebViewUri = "http://dailyapi.ibaozou.com/faq?category_id=1", DisplayType = "3" };
            NavigationHelper.DetailFrameNavigate(typeof(WebViewPage), paramter);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            Clipboard.ContentChanged -= Clipboard_ContentChanged;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(storage, value))
            {
                storage = value;
                OnPropertyChanged(propertyName);
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        
    }
}
