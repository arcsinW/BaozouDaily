using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaoZouRiBao.Http;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Views;
using Windows.System;
using BaoZouRiBao.Controls;
using Windows.Storage;
using BaoZouRiBao.UserControls;
using Windows.ApplicationModel.DataTransfer;

namespace BaoZouRiBao.ViewModel
{
    public class WebViewPageViewModel : ViewModelBase
    {
        public WebViewPageViewModel()
        {
            HtmlString = new StringBuilder();
        }

        #region Properties
        private Document document;

        public Document Document
        {
            get
            {
                return document;
            }

            set
            {
                document = value;
                OnPropertyChanged();
            }
        }


        private DocumentExtra documentExtra;

        public DocumentExtra DocumentExtra
        {
            get
            {
                return documentExtra;
            }

            set
            {
                documentExtra = value;
                OnPropertyChanged();
            }
        }


        private DocumentRelated documentRelated;

        public DocumentRelated DocumentRelated
        {
            get
            {
                return documentRelated;
            }

            set
            {
                documentRelated = value;
                OnPropertyChanged();
            }
        }


        private DocumentComment documentComment;

        public DocumentComment DocumentComment
        {
            get
            {
                return documentComment;
            }

            set
            {
                documentComment = value;
                OnPropertyChanged();
            }
        }


        public StringBuilder HtmlString { get; set; }

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


        private bool isBrowerEnable = false;

        /// <summary>
        /// 是否可以用浏览器打开
        /// </summary>
        public bool IsBrowerEnable
        {
            get { return isBrowerEnable; }
            set { isBrowerEnable = value; OnPropertyChanged(); }
        }
         
        #endregion

        #region WebView's events
        public void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            IsActive = true;
        }

        public void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            IsActive = false;
        } 
        #endregion

        public async void LoadDocument(string documentId, string displayType)
        {
            IsActive = true;
            DocumentExtra = await ApiService.Instance.GetDocumentExtraAsync(documentId);
            Document = await ApiService.Instance.GetDocumentAsync(documentId);
            switch (displayType)
            {
                case "1":
                    DocumentComment = await ApiService.Instance.GetDocumentCommentAsync(documentId);
                    DocumentRelated = await ApiService.Instance.GetDocumentRelatedAsync(documentId);
                    if (Document != null)
                    {
                        HtmlString.Clear();
                        HtmlString.Append(Document.Head).Append(Document.Body);
                        OnPropertyChanged(nameof(HtmlString));
                    }

                    IsBrowerEnable = true;
                    break;
                case "2": // pure html

                    break;
            } 

            IsActive = false;
        }
        

        /// <summary>
        /// 收藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Favorite(object sender, RoutedEventArgs e)
        {
            if (DocumentExtra != null)
            {
                if (DocumentExtra.Favorited)
                {
                    var res = await ApiService.Instance.UnFavoriteAsync(DocumentExtra.DocumentId);
                    if (res != null)
                    {
                        if (!string.IsNullOrEmpty(res.Count))
                        {
                            DocumentExtra.Favorited = false;
                            ToastService.SendToast("取消收藏成功");
                        }
                    }
                }
                else
                {
                    var res = await ApiService.Instance.FavoriteAsync(DocumentExtra.DocumentId);
                    if (res != null)
                    {
                        if (res.Status.Equals("1000"))
                        {
                            DocumentExtra.Favorited = true;
                            ToastService.SendToast("收藏成功");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void UnFavorite(object sender, RoutedEventArgs e)
        {
            if (DocumentExtra != null)
            {
                var res = await ApiService.Instance.UnFavoriteAsync(DocumentExtra.DocumentId);
                if (res != null)
                {
                    //if (res.Status.Equals("1000"))
                    //{
                    //    IsFavorite = true;
                    //    ToastService.SendToast("收藏成功");
                    //}
                }
            }
        }

        /// <summary>
        /// 点赞文章
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task Vote()
        {
            if (Document != null)
            {
                var result = await ApiService.Instance.VoteAsync(Document.DocumentId);
                if (result != null)
                {
                    if (result.Status.Equals("1000")) //点赞成功
                    {
                        DocumentExtra.Voted = true;
                        DocumentExtra.VoteCount = result.Data.Count;
                        OnPropertyChanged("DocumentExtra");
                    }
                    ToastService.SendToast(result.AlertDesc);
                }
            }

            VoteTask();
        }

        /// <summary>
        /// 完成点赞文章的任务
        /// </summary>
        public void VoteTask()
        {
            BaoZouTaskManager.VoteDocument();
        }

        /// <summary>
        /// 跳转到评论列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommentBtnClick(object sender, RoutedEventArgs e)
        {
            if (DocumentExtra != null)
            {
                NavigationHelper.DetailFrameNavigate(typeof(CommentPage), DocumentExtra.DocumentId);
            }
        }

        /// <summary>
        /// 浏览器打开
        /// </summary>
        public async void LaunchByBrower()
        {
            if (Document != null)
            {
                await Launcher.LaunchUriAsync(new Uri(Document.ShareUrl));
            }
        }

        /// <summary>
        /// 微信分享
        /// </summary>
        public async void WeChatShare()
        {  
            await WeChatHelper.ShareWebAsync(Document.Title, "", Document.ShareUrl);
        }

        /// <summary>
        /// 复制链接
        /// </summary>
        public void CopyLink()
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(Document.ShareUrl);
            Clipboard.SetContent(dataPackage);

            ToastService.SendToast("链接已复制到剪切板");
        }
    }
}
