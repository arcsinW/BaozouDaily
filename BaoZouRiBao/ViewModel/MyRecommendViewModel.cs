using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;

namespace BaoZouRiBao.ViewModel
{
    public class MyRecommendViewModel : ViewModelBase
    {
        public MyRecommendViewModel()
        {
            LoadShareUri();
            Clipboard.ContentChanged += Clipboard_ContentChanged;
        }

        ~MyRecommendViewModel()
        {
            Clipboard.ContentChanged -= Clipboard_ContentChanged;
        }

        private void Clipboard_ContentChanged(object sender, object e)
        {
            LoadShareUri();
        }

        /// <summary>
        /// 获取分享Uri
        /// </summary>
        public async void LoadShareUri()
        {
            DataPackageView dataPackage = Clipboard.GetContent();
            if (dataPackage.Contains(StandardDataFormats.Text))
            {
                ShareUri = await dataPackage.GetTextAsync();
                await GetTitleByUri();

                Uri result = null;
                Uri.TryCreate(ShareUri, UriKind.RelativeOrAbsolute, out result);
                
                if (result != null)
                {
                    IsValid = true;
                }
            }
        }

        /// <summary>
        /// 投稿
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Contribute(object sender, RoutedEventArgs e)
        {
            var result = await ApiService.Instance.UserContributeAsync(ShareUri, Title);
            if (result != null)
            {
                if (result.Status.Equals("1000"))
                {
                    ToastService.SendToast("投稿成功");
                }
            }
        }

        /// <summary>
        /// 通过Uri获取Title
        /// </summary>
        /// <returns></returns>
        private async Task GetTitleByUri()
        {
            try
            {
                string html = await new HttpClient().GetStringAsync(ShareUri);
                Regex regex = new Regex(@"(?<=<title>)[\s\S]*?(?=</title>)");
                MatchCollection matches = regex.Matches(html);
                for (int i = 0; i < matches.Count; i++)
                {
                    if (matches[i].Success)
                    {
                        if (!string.IsNullOrEmpty(matches[i].Value))
                        {
                            Title = WebUtility.HtmlDecode(matches[i].Value).Trim();
                            return;
                        }
                    }
                }
                
                Title = ShareUri;
            }
            catch
            {
                Title = ShareUri;
            }
        }

        #region Properties
        private bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged();
            }
        }

        private string shareUri = "请先复制你想推荐的链接";
        public string ShareUri
        {
            get
            {
                return shareUri;
            }
            set
            {
                shareUri = value;
                OnPropertyChanged();
            }
        }

        private bool isValid = false;
        /// <summary>
        /// Uri是否合法
        /// </summary>
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
                OnPropertyChanged();
            }
        }
        
        private string title = "链接标题";
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
