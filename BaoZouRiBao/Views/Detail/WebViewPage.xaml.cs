using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Model;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as WebViewParameter;
            if (parameter != null)
            {
                string documentId = parameter.DocumentId;
                string displayType = parameter.DisplayType;
                if (!string.IsNullOrEmpty(parameter.Title))
                {
                    titleTextBlock.Text = parameter.Title;
                }

                switch (parameter.DisplayType)
                {
                    case "1":

                        break;
                    case "2":   // pure html
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
    }
}
