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


        private bool isFavorite;

        public bool IsFavorite
        {
            get { return isFavorite; }
            set { isFavorite = value; OnPropertyChanged(); }
        }

        #endregion

        public void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            IsActive = true;
        }

        public void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args) 
        {
            IsActive = false;
        }

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
            IsFavorite = Document.Favorited;
            IsActive = false;
        }

        private async Task<VoteOperationResult> Favorite(string documentId)
        {
            var res = await ApiService.Instance.FavoriteAsync(documentId);
            return res;
        }

        /// <summary>
        /// 收藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void FavoriteBtnClick(object sender, RoutedEventArgs e)
        {
            if (DocumentExtra != null)
            {
                var res = await Favorite(DocumentExtra.DocumentId.ToString());
                if (res != null)
                {
                    if (res.Status.Equals("2006"))
                    {
                        IsFavorite = true;
                    }
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
                    DocumentExtra.VoteCount = result.Data.Count;
                }
                ToastService.SendToast(result.alertDesc);
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
            if(Document != null)
            {
                await Launcher.LaunchUriAsync(new Uri(Document.ShareUrl));
            }
        }
    }
}
