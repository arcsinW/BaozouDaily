using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class MessagePageViewModel : ViewModelBase
    {
        public MessagePageViewModel()
        {
            if(IsDesignMode)
            {
                LoadDesignData();
            }

            CommentMessages = new IncrementalLoadingList<Message>(LoadCommentMessages, () => { IsActive = false; }, () => { IsActive = true; });
            VoteMessages = new IncrementalLoadingList<Message>(LoadVoteMessage, () => { IsActive = false; }, () => { IsActive = true; });
            AdminMessages = new IncrementalLoadingList<Message>(LoadAdminMessage, () => { IsActive = false; }, () => { IsActive = true; });
        }
         
        private void LoadDesignData()
        {

        }

        #region Load data methods
        public async Task<IEnumerable<Message>> LoadCommentMessages(uint count, string timeStamp)
        {
            List<Message> messages = new List<Message>();

            if (timeStamp.Equals("0"))
            {
                CommentMessages.NoMore();
                return messages;
            }

            var result = await ApiService.Instance.GetCommentMessages();
            if (result != null && result.CommentMessages != null)
            {
                CommentMessages.TimeStamp = result.TimeStamp;
                foreach (var item in result.CommentMessages)
                {
                    messages.Add(item);
                }
            }

            if (messages.Count == 0 && CommentMessages.Count == 0)
            {
                IsCommentMessageEmpty = true;
            }
            else
            {
                IsCommentMessageEmpty = false;
            }

            return messages;
        }

        public async Task<IEnumerable<Message>> LoadVoteMessage(uint count, string timeStamp)
        { 
            List<Message> messages = new List<Message>();

            if (timeStamp.Equals("0"))
            {
                VoteMessages.NoMore();
                return messages;
            }

            var result = await ApiService.Instance.GetCommentVoteMessages();
            if (result != null && result.CommentMessages != null)
            {
                VoteMessages.TimeStamp = result.TimeStamp;
                foreach (var item in result.CommentMessages)
                {
                    messages.Add(item);
                }
            }

            if (messages.Count == 0 && VoteMessages.Count == 0)
            {
                IsVoteMessageEmpty = true;
            }
            else
            {
                IsVoteMessageEmpty = false;
            }

            return messages; 
        }

        public async Task<IEnumerable<Message>> LoadAdminMessage(uint count, string timeStamp)
        { 
            List<Message> messages = new List<Message>();

            if (timeStamp.Equals("0"))
            {
                AdminMessages.NoMore();
                return messages;
            }

            var result = await ApiService.Instance.GetAdminMessages();
            if (result != null && result.AdminMessages != null)
            {
                CommentMessages.TimeStamp = result.TimeStamp;
                foreach (var item in result.AdminMessages)
                {
                    messages.Add(item);
                }
            }

            if (messages.Count == 0 && AdminMessages.Count == 0)
            {
                IsAdminMessageEmpty = true;
            }
            else
            {
                IsAdminMessageEmpty = false;
            }

            return messages;
        }  
        #endregion

        #region Properties

        /// <summary>
        /// 评论的消息
        /// </summary>
        public IncrementalLoadingList<Message> CommentMessages { get; set; }
         
        /// <summary>
        /// 赞的消息
        /// </summary>
        public IncrementalLoadingList<Message> VoteMessages { get; set; }

        /// <summary>
        /// 系统消息
        /// </summary>
        public IncrementalLoadingList<Message> AdminMessages { get; set; }

        private bool isActive = false;
        /// <summary>
        /// 是否正在加载数据
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        }

        private bool isVoteMessageEmtpy;
        /// <summary>
        /// 赞 信息是否为空
        /// </summary>
        public bool IsVoteMessageEmpty
        {
            get { return isVoteMessageEmtpy; }
            set { isVoteMessageEmtpy = value; OnPropertyChanged(); }
        }

        private bool isAdminMessageEmpty;
        /// <summary>
        /// 系统消息是否为空
        /// </summary>
        public bool IsAdminMessageEmpty
        {
            get { return isAdminMessageEmpty; }
            set { isAdminMessageEmpty = value; OnPropertyChanged(); }
        }

        private bool isCommentMessageEmpty;
        /// <summary>
        /// 评论消息是否为空
        /// </summary>
        public bool IsCommentMessageEmpty
        {
            get { return isCommentMessageEmpty; }
            set { isCommentMessageEmpty = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
