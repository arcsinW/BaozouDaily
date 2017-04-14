using BaoZouRiBao.Enums;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class CommentPageViewModel : ViewModelBase
    {
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
        #endregion

        #region Fields
        private string documentId;
        #endregion

        public void SetDocumentId(string documentId)
        {
            this.documentId = documentId;
        }

        public CommentPageViewModel()
        {
            if(IsDesignMode)
            {
                SetDocumentId("44791");
                GetLatestComments();
                GetHotComments();
            }
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
        public void Vote(string commentId)
        {
            
        }

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="commentId"></param>
        public void ReplayComment(string commentId)
        {

        }
    }
}
