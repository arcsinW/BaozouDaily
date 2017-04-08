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

        #endregion

        public void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            IsActive = true;
        }

        public void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args) 
        {
            IsActive = false;
        }

        public async void LoadDocument(string documentId,string displayType)
        {
            IsActive = true;
            DocumentExtra = await ApiService.Instance.GetDocumentExtra(documentId);
            switch (displayType)
            {
                case "1":
                    Document = await ApiService.Instance.GetDocument(documentId);
                    DocumentComment = await ApiService.Instance.GetDocumentComment(documentId);
                    DocumentRelated = await ApiService.Instance.GetDocumentRelated(documentId);
                    if (Document != null)
                    {
                        HtmlString.Clear();
                        HtmlString.Append(Document.Head).Append(Document.Body);
                        OnPropertyChanged(nameof(HtmlString));
                    }

                    break;
                case "2": // pure html

                    break;
            }

            IsActive = false;
        }

        public async Task<OperationResult> Favorite(string documentId)
        {
            var res = await ApiService.Instance.Favorite(Document.DocumentId);
            return res;
        }

        public async void FavoriteBtnClick(object sender, RoutedEventArgs e)
        {
            if (DocumentExtra == null)
            {
                return;
            }

            var res = await Favorite(DocumentExtra.DocumentId.ToString());
        }

        public void CommentBtnClick(object sender, RoutedEventArgs e)
        {
            if (DocumentExtra == null)
            {
                return;
            }

            NavigationHelper.DetailFrameNavigate(typeof(CommentPage), DocumentExtra.DocumentId);
        }
    }
}
