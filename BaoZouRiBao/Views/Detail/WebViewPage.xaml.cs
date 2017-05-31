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
using Windows.ApplicationModel.DataTransfer;

namespace BaoZouRiBao.Views
{
    public sealed partial class WebViewPage : Page
    {
        public WebViewPage()
        {
            this.InitializeComponent();

            appTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.DataChanged += Current_DataChanged;
        }

        private void Current_DataChanged()
        {
            appTheme = DataShareManager.Current.AppTheme;
            if (appTheme == ElementTheme.Dark)
            {
                Dark();
            }
            else
            {
                Light();
            }
        }

        #region Fields
        private DocumentExtra documentExtra;

        private ElementTheme appTheme;

        private WebViewParameter parameter;
        #endregion
        
        #region Js Bridge
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
        #endregion

        #region Navigation mehods
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested += WebViewPage_DataRequested;
            parameter = e.Parameter as WebViewParameter;
            if (parameter != null)
            {
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
        
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested -= WebViewPage_DataRequested;
            DataShareManager.Current.DataChanged -= Current_DataChanged;
        } 
        #endregion

        #region Share
        private void shareBtn_Click(object sender, RoutedEventArgs e)
        {
            shareDialog.Show(); 
        }

        private void WebViewPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var deferral = args.Request.GetDeferral();

            args.Request.Data.Properties.Title = parameter.Title;
            if (ViewModel.Document != null)
            {
                args.Request.Data.SetWebLink(new Uri(ViewModel.Document.ShareUrl));
            }

            deferral.Complete();
        }

        /// <summary>
        /// 微信分享
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shareDialog_WechatClick(object sender, RoutedEventArgs e)
        {
            ViewModel.WeChatShare();
        }

        /// <summary>
        /// 微博分享
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shareDialog_WeiboClick(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// 复制链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shareDialog_LinkClick(object sender, RoutedEventArgs e)
        {
            ViewModel.CopyLink();
        }

        /// <summary>
        /// 系统分享
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shareDialog_MoreClick(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        } 
        #endregion
    }
}
