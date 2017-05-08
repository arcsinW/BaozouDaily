using BaoZouRiBao.Common;
using BaoZouRiBao.Controls;
using BaoZouRiBao.Enums;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BaoZouRiBao.ViewModel
{
    public class CommentPageViewModel : ViewModelBase
    {
        public CommentPageViewModel()
        {
            if (IsDesignMode)
            {
                SetDocumentId("44791");
                GetLatestComments();
                GetHotComments();
            }

            CommentCommand = new RelayCommand((str) => { ReplyCommentAsync((string)str, Content); },()=> { return !string.IsNullOrEmpty(Content); });
            VoteCommand = new RelayCommand((str) => { VoteAsync((string)str); });
        }

        #region Properties
        public CommentCollection LatestComments { get; set; }

        public CommentCollection HotComments { get; set; }
       
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

        private string content;
        /// <summary>
        /// 评论的内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 评论 Command
        /// </summary>
        public RelayCommand CommentCommand { get; set; }

        /// <summary>
        /// 点赞 Command
        /// </summary>
        public RelayCommand VoteCommand { get; set; }
        #endregion

        #region Fields
        private string documentId;
        #endregion

        public void SetDocumentId(string documentId)
        {
            this.documentId = documentId;
        }
         
        private void OnDataLoadingEvent()
        {
            IsActive = true;
        }

        private void OnDataLoadedEvent()
        {
            IsActive = false;
        }

        public void GetLatestComments()
        {
            LatestComments = new CommentCollection(CommentTypeEnum.latest, documentId);
            LatestComments.OnDataLoadedEvent += OnDataLoadedEvent;
            LatestComments.OnDataLoadingEvent += OnDataLoadingEvent;
        }

        public void GetHotComments()
        {
            HotComments = new CommentCollection(CommentTypeEnum.hot, documentId);
            HotComments.OnDataLoadingEvent += OnDataLoadingEvent;
            HotComments.OnDataLoadedEvent += OnDataLoadedEvent;
        }

        public void RefreshLatestComments()
        {
            CommentCollection c = new CommentCollection(CommentTypeEnum.latest,documentId);
            LatestComments = c;
        }

        public void RefreshHotComments()
        {
            CommentCollection c = new CommentCollection(CommentTypeEnum.hot,documentId);
            HotComments = c;
        }

        /// <summary>
        /// 给评论点赞
        /// </summary>
        /// <param name="commentId"></param>
        public async void VoteAsync(string commentId)
        {
            var result = await ApiService.Instance.VoteAsync(commentId);

        }

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="commentId"></param>
        public async void ReplyCommentAsync(string commentId, string content)
        {
            var result = await ApiService.Instance.CommentAsync(commentId, content);

        }

        /// <summary>
        /// 评论文章
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task Comment()
        {
            var result = await ApiService.Instance.CommentAsync(documentId, Content);
            if (result != null && !string.IsNullOrEmpty(result.Id))
            {
                ToastService.SendToast("评论成功");
            }
        }


        public async void replyBtn_Click(object sender, RoutedEventArgs e)
        {
            Comment dataContext = (Comment)((Button)e.OriginalSource).DataContext;
            ReplyCommentAsync(dataContext.Id, Content);
        }

        public async void likeBtn_Click(object sender, RoutedEventArgs e)
        {
            Comment dataContext = (Comment)((Button)e.OriginalSource).DataContext;
            VoteAsync(dataContext.Id);
        }
    }
}
