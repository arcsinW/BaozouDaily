using BaoZouRiBao.Model;
using BaoZouRiBao.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{
    public sealed partial class WebViewPage : Page
    {
        public WebViewPage()
        {
            this.InitializeComponent();
        }

        private StringBuilder documentId = new StringBuilder();
        private string displayType;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as WebViewParameter;
            if (parameter != null)
            {
                documentId.Clear();
                documentId.Append(parameter.DocumentId);
                displayType = parameter.DisplayType;
                if(!string.IsNullOrEmpty(parameter.Title))
                {
                    titleTextBlock.Text = parameter.Title;
                }
                switch (parameter.DisplayType)
                {   
                    case "1":

                        break;
                    case "2":   //pure html
                        webView.Navigate(new Uri(parameter.WebViewUri));
                        break;
                    case "3":
                        webView.Navigate(new Uri(parameter.WebViewUri));
                        stackPanel.Visibility = Visibility.Collapsed;
                        return;
                }
                ViewModel.LoadDocument(documentId.ToString(), displayType);
            }
        } 

        private async void FavoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            var res = await ViewModel.Favorite(documentId.ToString());
        }

        private void CommentBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.DetailFrameNavigate(typeof(CommentPage), documentId.ToString());
        } 
    }
}
