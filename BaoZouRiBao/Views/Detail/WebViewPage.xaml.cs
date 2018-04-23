using System;
using System.Collections.Generic;
using System.Diagnostics;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using BaoZouRiBao.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using BaoZouRiBao.UserControls;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Storage;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using BaoZouRiBao.Controls;
using BaoZouRiBao.Cache;

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

        public ObservableCollection<JsImage> Images { get; set; } = new ObservableCollection<JsImage>();

        #region Fields
        private DocumentExtra documentExtra;

        private ElementTheme appTheme;

        private WebViewParameter parameter;

        private DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();

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
            parameter = e.Parameter as WebViewParameter;
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DataShareManager.Current.DataChanged -= Current_DataChanged;
        }
        #endregion

        #region Share
        private void shareBtn_Click(object sender, RoutedEventArgs e)
        {
            ShareDialog dialog = new ShareDialog();
            dialog.WechatClick += (s, a) =>
            {
                ViewModel.WeChatShare();
            };

            dialog.LinkClick += (s, a) =>
            {
                ViewModel.CopyLink();
            };

            dialog.MoreClick += (s, a) =>
            {
                dataTransferManager.DataRequested += WebViewPage_DataRequested;
                DataTransferManager.ShowShareUI();
            };

            dialog.Show();
        }

        private void Dialog_MoreClick(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
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
            dataTransferManager.DataRequested += WebViewPage_DataRequested;
            DataTransferManager.ShowShareUI();
            //dataTransferManager.DataRequested -= WebViewPage_DataRequested;
        }


        #endregion

        #region WebView's events

        public void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            progressRing.IsActive = true;
        }

        public async void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            progressRing.IsActive = false;

            var js = @"
               
                var imgs = document.getElementsByTagName('img');
                var images = new Array();
                for (var i = 0; i < imgs.length; i++)
                {
                    var img = new Object();
                    img.src = imgs[i].src;
                    img.index = i+1;
                    images.push(img);
                    let indexTmp = i;
                    imgs[i].onclick = function (e){
                        window.external.notify('onclick:' + indexTmp ); //this.src                        

                        //var obj = {type: 'image', src : this.src};
                        // window.external.notify(JSON.stringify(obj));
                        //var json = ""{'type':'onclick','src':"" + this.src + "",'index':""+ this.index + ""}"";
                        //window.external.notify('type : onclick:' +  ); //this.src
                        //window.external.notify(json); //this.src
                    };
                }
                
                window.external.notify(JSON.stringify(images));";

            string json = await sender.InvokeScriptAsync("eval", new[] { js });
        }

        public void WebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Value);

            if (!string.IsNullOrEmpty(e.Value))
            {
                if (e.Value.StartsWith("onclick:"))
                {
                    string url = e.Value.Replace("onclick:", string.Empty);
                    int selectedIndex = Convert.ToInt32(url);
                    imageFlipView.Visibility = Visibility.Visible;
                    imageFlipView.SelectedIndex = selectedIndex;
                    commandBar.Visibility = Visibility.Visible;
                    topPop.IsOpen = false;
                }
                else
                {
                    var imgs = JsonHelper.Deserlialize<List<JsImage>>(e.Value);
                    Images.Clear();
                    imgs.ForEach(img => Images.Add(img));
                }
            }
        }
        #endregion

        private void imageFlipView_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            CloseImageFlipView();
        }

        public void CloseImageFlipView()
        {
            if (imageFlipView.Visibility == Visibility.Visible)
            {
                imageFlipView.Visibility = Visibility.Collapsed;
                commandBar.Visibility = Visibility.Collapsed;
                topPop.IsOpen = true;
            }
            else
            {
                if (Frame.CanGoBack)
                {
                    this.Frame.GoBack();
                }
            }
        }

        public async Task ShareImage()
        {
            if (imageFlipView.SelectedItem is JsImage img)
            {
                if (!string.IsNullOrEmpty(img.Src))
                {
                    try
                    {
                        var file = await ImageCache.Instance.GetFileFromCacheAsync(new Uri(img.Src));

                        dataTransferManager.DataRequested -= WebViewPage_DataRequested;
                        //dataTransferManager.DataRequested += WebViewPage_ImageDataRequested;

                        dataTransferManager.DataRequested += (sender, args) =>
                        {
                            var deferral = args.Request.GetDeferral();

                            List<IStorageItem> imgs = new List<IStorageItem>() { file };
                            args.Request.Data.SetStorageItems(imgs);
                            
                            RandomAccessStreamReference imgRef = RandomAccessStreamReference.CreateFromFile(file);
                            args.Request.Data.Properties.Thumbnail = imgRef;
                            args.Request.Data.SetBitmap(imgRef);

                            deferral.Complete();
                        };

                        DataTransferManager.ShowShareUI();
                    }
                    catch(Exception e)
                    {
                        ToastService.SendToast("分享失败" + e.Message);
                    }
                }
            }

        }

        public async Task SaveImage()
        {
            var folder = KnownFolders.SavedPictures; //.CreateFolderAsync("暴走日报", CreationCollisionOption.OpenIfExists);
            var date = DateTime.Now;
            StorageFile file = await folder.CreateFileAsync($"{date.Year}-{date.Month}-{date.Day}_{date.Hour}_{date.Minute}_{date.Second}.jpg");


            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                try
                {
                    if (imageFlipView.SelectedItem is JsImage img)
                    {
                        if (!string.IsNullOrEmpty(img.Src))
                        {
                            using (Stream stream = await file.OpenStreamForWriteAsync())
                            {
                                IBuffer buffer = await HttpBaseService.GetBytesAsync(img.Src);
                                stream.Write(buffer.ToArray(), 0, (int)buffer.Length);
                                await stream.FlushAsync();
                            }
                            FileUpdateStatus updateStatus = await CachedFileManager.CompleteUpdatesAsync(file);
                            if (updateStatus == FileUpdateStatus.Complete)
                            {
                                
                                int index = folder.Path.LastIndexOf('\\');
                                string path = folder.Path.Substring(0, index) + '\\' + folder.DisplayName;
                                ToastService.SendToast("已保存到 " + path);
                            }
                        } 
                    } 
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
