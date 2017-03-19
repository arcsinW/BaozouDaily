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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace BaoZouRiBao.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
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
                switch (parameter.DisplayType)
                {   
                    case "1":

                        break;
                    case "2":   //pure html
                        webView.Navigate(new Uri(parameter.WebViewUri));
                        if (parameter.Title != null)
                        {
                            titleTextBlock.Text = parameter.Title;
                        }
                        break;
                    case "3":
                        webView.Navigate(new Uri(parameter.WebViewUri));
                        if (parameter.Title != null)
                        {
                            titleTextBlock.Text = parameter.Title;
                        }
                        stackPanel.Visibility = Visibility.Collapsed;
                        return;
                        
                }
                ViewModel.LoadDocument(documentId.ToString(), displayType);
            }
        }

        //private async void webView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        //{
        //    //var js = @"document.body.style.overflow = 'hidden';
        //   var js = @"window.external.notify(JSON.stringify(document.body.scrollHeight));";

        //    await webView.InvokeScriptAsync("eval", new[] { js });
        //}

        //private void webView_ScriptNotify(object sender, NotifyEventArgs e)
        //{
        //    Debug.WriteLine(e.Value);
        //    webView.Height = Convert.ToDouble(e.Value);
        //    webView.Height = Convert.ToDouble(e.Value);
        //}

        private async void favoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            var res = await ViewModel.Favorite(documentId.ToString());
            
        }

        private void commentBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.DetailFrameNavigate(typeof(CommentPage), documentId.ToString());
        }
    }
}
