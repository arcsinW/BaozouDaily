using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
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

        private async void Clipboard_ContentChanged(object sender, object e)
        {
            DataPackageView dataPackage = Clipboard.GetContent();
            if (dataPackage.Contains(StandardDataFormats.Text))
            {
                ShareUri = await dataPackage.GetTextAsync();
                await GetTitleByUri();
            } 
        }

        public async void LoadShareUri()
        {
            DataPackageView dataPackage = Clipboard.GetContent();
            if (dataPackage.Contains(StandardDataFormats.Text))
            {
                ShareUri = await dataPackage.GetTextAsync();
                await GetTitleByUri();
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
                if (result.Status.Equals("success"))
                {

                }
            }
        }

        private async Task GetTitleByUri()
        {
            try
            {
                string html = await new HttpClient().GetStringAsync(ShareUri);
                //Regex regex = new Regex("(?<=<title>)(.*?)(?=</title>)");
                Regex regex = new Regex(@"(?<=<title>)[\s\S]*?(?=</title>)");
                var match = regex.Match(html);
                if (match.Success)
                {
                    Title = WebUtility.HtmlDecode(match.Value).Trim();
                }
                else
                {
                    Title = ShareUri;
                }
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
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
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
