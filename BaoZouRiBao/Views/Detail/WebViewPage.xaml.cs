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
using BaoZouRiBao.Http;

namespace BaoZouRiBao.Views
{
    public sealed partial class WebViewPage : Page
    {
        public WebViewPage()
        {
            this.InitializeComponent();

            GlobalValue.Current.ThemeChanged += Current_ThemeChanged;
        }

        private DocumentExtra documentExtra;

        private void Current_ThemeChanged(object sender, bool e)
        {
            // Dark -> Light
            if (e)
            {
                Light();
            }
            // Light -> Dark
            else
            {
                Dark();
            }
        }

        public async void Light()
        {
            if (documentExtra != null && documentExtra.HackJs != null && !string.IsNullOrEmpty(documentExtra.HackJs.SetDayMode))
            {
                await webView.InvokeScriptAsync("eval", new string[] { documentExtra.HackJs.SetDayMode });
            }
        }

        public async void Dark()
        {
            if (documentExtra != null && documentExtra.HackJs != null && !string.IsNullOrEmpty(documentExtra.HackJs.SetNightMode))
            {
                await webView.InvokeScriptAsync("eval", new string[] { documentExtra.HackJs.SetNightMode });
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as WebViewParameter;
            if (parameter != null)
            {
                if (!string.IsNullOrEmpty(parameter.Title))
                {
                    titleTextBlock.Text = parameter.Title;
                }

                switch (parameter.DisplayType)
                {
                    case "1":

                        break;
                    case "2":   // pure html
                        if (!string.IsNullOrEmpty(parameter.WebViewUri))
                        {
                            webView.Navigate(new Uri(parameter.WebViewUri));
                        }
                        break;
                    case "3":
                        if (!string.IsNullOrEmpty(parameter.WebViewUri))
                        {
                            webView.Navigate(new Uri(parameter.WebViewUri));
                        }
                        stackPanel.Visibility = Visibility.Collapsed;
                        return;
                }

                string documentId = parameter.DocumentId;
                if (!string.IsNullOrEmpty(documentId))
                {
                    ViewModel.LoadDocument(documentId, parameter.DisplayType);


                    documentExtra = await ApiService.Instance.GetDocumentExtraAsync(documentId);
                    if (documentExtra != null && documentExtra.HackJs != null)
                    {
                        if (!string.IsNullOrEmpty(documentExtra.HackJs.DocumentLoaded))
                        {
                            await webView.InvokeScriptAsync("eval", new string[] { documentExtra.HackJs.DocumentLoaded });
                        }

                        if (this.RequestedTheme == ElementTheme.Light)
                        {
                            Light();
                        }
                        else
                        {
                            Dark();
                        }
                    }
                }
            }
        }
    }
}
