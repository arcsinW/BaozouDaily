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
using System.Threading.Tasks;

namespace BaoZouRiBao.Views
{
    public sealed partial class WebViewPage : Page
    {
        public WebViewPage()
        {
            this.InitializeComponent();

            appTheme = GlobalValue.Current.AppTheme;
            GlobalValue.Current.DataChanged += Current_DataChanged;
        }

        private ElementTheme appTheme;

        private void Current_DataChanged()
        {
            appTheme = GlobalValue.Current.AppTheme;
            if (appTheme == ElementTheme.Dark)
            {
                Dark();
            }
            else
            {
                Light();
            }
        }

        private DocumentExtra documentExtra;
        
        public async void Light()
        {
            if (documentExtra != null && documentExtra.HackJs != null && !string.IsNullOrEmpty(documentExtra.HackJs.SetDayMode))
            {
                await InvokeScriptAsync("eval", new string[] { documentExtra.HackJs.SetDayMode });
            }
        }

        public async void Dark()
        {
            if (documentExtra != null && documentExtra.HackJs != null && !string.IsNullOrEmpty(documentExtra.HackJs.SetNightMode))
            {
                await InvokeScriptAsync("eval", new string[] { documentExtra.HackJs.SetNightMode });
            }
        }

        /// <summary>
        /// Invoke js
        /// </summary>
        /// <param name="funcName">function name of js</param>
        /// <param name="args">arguments of js </param>
        public async Task<string> InvokeScriptAsync(string funcName, IEnumerable<string> args)
        {
            try
            {
                return await webView?.InvokeScriptAsync(funcName, args);
            }
            catch (Exception ex)
            {
                string errorText = string.Empty;
                switch (ex.HResult)
                {
                    case unchecked((int)0x80020006):
                        errorText = $"There is no function called {funcName}";
                        break;
                    case unchecked((int)0x80020101):
                        errorText = $"A JavaScript error or exception occured while executing the function {funcName}";
                        break;
                    case unchecked((int)0x800a138a):
                        errorText = $"{funcName} is not a function";
                        break;
                    default:
                        // Some other error occurred.
                        errorText = funcName + ex.Message;
                        break;
                }
                Debug.WriteLine(errorText);
                return string.Empty;
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
                            await InvokeScriptAsync("eval", new string[] { documentExtra.HackJs.DocumentLoaded });
                        }

                        if (appTheme == ElementTheme.Dark)
                        {
                            Dark();
                        }
                        else
                        {
                            Light();
                        }
                    }
                }
            }
        }
    }
}
