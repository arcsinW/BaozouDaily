using BaoZouRiBao.Common;
using BaoZouRiBao.Controls;
using BaoZouRiBao.Enums;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
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
                DocumentId = "44791";
            }

            LatestComments = new IncrementalLoadingList<Comment>(LoadLatestComments, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(e.Message); IsActive = false; });
            HotComments = new IncrementalLoadingList<Comment>(LoadHotComments, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(e.Message); IsActive = false; });

            CommentCommand = new RelayCommand
            (
                async (commentId) =>
                {
                    await ReplyCommentAsync((string)commentId, Content);
                }, 
                () =>
                {
                    return !string.IsNullOrEmpty(Content);
                }
            );

            VoteCommand = new RelayCommand
            (
                (commentId) =>
                {
                    VoteAsync((string)commentId);
                }
            );

            ReportCommand = new RelayCommand
            (
                (commentId) =>
                {
                    ReportAsync((string)commentId);
                }
            );
        }

        
        #region Properties
        /// <summary>
        /// 最新评论
        /// </summary>
        public IncrementalLoadingList<Comment> LatestComments { get; set; }

        /// <summary>
        /// 最热门评论
        /// </summary>
        public IncrementalLoadingList<Comment> HotComments { get; set; }

        private bool isActive = false;
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
        /// 日报Id
        /// </summary>
        public string DocumentId { get; set; }

        /// <summary>
        /// 当前PivotIndex
        /// </summary>
        public int CurrentIndex { get; set; }

        private bool isHotCommentsEmpty = false;
        /// <summary>
        /// 热门评论是否为空
        /// </summary>
        public bool IsHotCommentsEmpty
        {
            get { return isHotCommentsEmpty; }
            set { isHotCommentsEmpty = value; OnPropertyChanged(); }
        }

        private bool isLatestCommentsEmpty = false;
        /// <summary>
        /// 最新评论是否为空
        /// </summary>
        public bool IsLatestCommentsEmpty
        {
            get { return isLatestCommentsEmpty; }
            set { isLatestCommentsEmpty = value; OnPropertyChanged(); }
        }
         
        #endregion

        #region Commands

        /// <summary>
        /// 评论 Command
        /// </summary>
        public RelayCommand CommentCommand { get; set; }

        /// <summary>
        /// 点赞 Command
        /// </summary>
        public RelayCommand VoteCommand { get; set; }

        /// <summary>
        /// 举报 Command
        /// </summary>
        public RelayCommand ReportCommand { get; set; }
        #endregion

        #region Load data methods
        public StringBuilder latestCommentsStringBuilder = new StringBuilder("0");

        /// <summary>
        /// 加载最新评论
        /// </summary>
        /// <param name="cancel"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Comment>> LoadLatestComments(uint count, string timeStamp)
        {
            if (timeStamp.Equals(latestCommentsStringBuilder.ToString()) || timeStamp.Equals("0"))
            {
                LatestComments.NoMore();
                return null;
            }

            var result = await ApiService.Instance.GetLatestOrHotCommentsAsync(DocumentId, CommentTypeEnum.latest, timeStamp);
            if (result != null && result.Comments != null)
            { 
                if (!string.IsNullOrEmpty(timeStamp))
                {
                    latestCommentsStringBuilder.Clear();
                    latestCommentsStringBuilder.Append(timeStamp);
                }

                LatestComments.TimeStamp = result.TimeStamp;
                
                if (result.Comments.Count == 0 && LatestComments.Count == 0)
                {
                    IsLatestCommentsEmpty = true;
                } 
                else
                {
                    IsLatestCommentsEmpty = false;
                }

                return result.Comments;
            }
            else
            {
                LatestComments.NoMore();

                if (LatestComments.Count == 0)
                {
                    IsLatestCommentsEmpty = true;
                }
                else
                {
                    IsLatestCommentsEmpty = false;
                }
            }

            return null;
        }


        public StringBuilder hotCommentsStringBuilder = new StringBuilder("0");

        /// <summary>
        /// 加载热门评论
        /// </summary>
        /// <param name="count"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Comment>> LoadHotComments(uint count, string timeStamp)
        {
            if (timeStamp.Equals(hotCommentsStringBuilder.ToString()) || timeStamp.Equals("0"))
            {
                HotComments.NoMore();
                return null;
            }
            
            var result = await ApiService.Instance.GetLatestOrHotCommentsAsync(DocumentId, CommentTypeEnum.hot, timeStamp);
            if (result != null && result.Comments != null)
            {
                if (!string.IsNullOrEmpty(timeStamp))
                {
                    hotCommentsStringBuilder.Clear();
                    hotCommentsStringBuilder.Append(timeStamp);
                }
                 
                HotComments.TimeStamp = result.TimeStamp;

                if (result.Comments.Count ==0 && HotComments.Count == 0)
                {
                    IsHotCommentsEmpty = true;
                }
                 
                return result.Comments;
            }
            else
            {
                HotComments.NoMore();

                if (HotComments.Count == 0)
                {
                    IsHotCommentsEmpty = true;
                }
                else
                {
                    IsHotCommentsEmpty = false;
                }
            }

            return null;
        }
        #endregion
         
        #region Refresh methods
        /// <summary>
        /// 刷新最新评论
        /// </summary>
        public async void RefreshLatestComments()
        {
            await LatestComments.ClearAndReloadAsync();
        }

        /// <summary>
        /// 刷新热门评论
        /// </summary>
        public async void RefreshHotComments()
        {
            await HotComments.ClearAndReloadAsync();
        } 
        #endregion

        #region Public methods

        public void GetLatestComments()
        {
            
        }

        public void GetHotComments()
        {
            
        }

        /// <summary>
        /// 给评论点赞
        /// </summary>
        /// <param name="commentId"></param>
        public async void VoteAsync(string commentId)
        {
            var result = await ApiService.Instance.VoteCommentAsync(commentId);
            if (result != null)
            {
                if (result.Status == "1000")
                {
                    ToastService.SendToast(result.AlertDesc);
                }
            }
        }

        /// <summary>
        /// 举报评论
        /// </summary>
        /// <param name="commentId"></param>
        public async void ReportAsync(string commentId)
        {
            var result = await ApiService.Instance.ReportCommentAsync(commentId);
            if (result != null)
            {
                if (result.Status == "1000")
                {
                    ToastService.SendToast(result.AlertDesc);
                }
            }
        }

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="parentId">评论Id</param>
        public async Task<CommentOperationResult> ReplyCommentAsync(string parentId)
        {
            var result = await ApiService.Instance.ReplyCommentAsync(DocumentId, parentId, Content);
            return result;
        }

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="parentId">评论Id</param>
        public async Task<CommentOperationResult> ReplyCommentAsync(string parentId, string content)
        {
            var result = await ApiService.Instance.ReplyCommentAsync(DocumentId, parentId, content);
            return result;
        }

        /// <summary>
        /// 评论文章
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CommentAsync()
        {
            var result = await ApiService.Instance.CommentAsync(DocumentId, Content);
            if (result != null && !string.IsNullOrEmpty(result.Id))
            {
                Comment comment = new Comment() { Content = result.Content, Time = result.time, Likes = result.Likes, Dislikes = result.Dislikes, User = result.user };
                LatestComments.Insert(0, comment);

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
