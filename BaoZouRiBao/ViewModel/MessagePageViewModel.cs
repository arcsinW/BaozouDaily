using BaoZouRiBao.Controls;
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

            CommentMessages = new IncrementalLoadingList<Message>(LoadCommentMessages, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(e.Message); });
            VoteMessages = new IncrementalLoadingList<Message>(LoadVoteMessage, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(e.Message); });
            AdminMessages = new IncrementalLoadingList<Message>(LoadAdminMessage, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(e.Message); });
        }
         
        private void LoadDesignData()
        {

        }

        #region Load data methods
        private StringBuilder commentMessageStringBuilder = new StringBuilder("0");

        public async Task<IEnumerable<Message>> LoadCommentMessages(uint count, string timeStamp)
        {
            List<Message> messages = new List<Message>();
            
            System.Diagnostics.Debug.WriteLine($"Previous timestamp : {commentMessageStringBuilder}");
            System.Diagnostics.Debug.WriteLine($"Current timestamp : {timeStamp}");

            if (timeStamp.Equals(commentMessageStringBuilder.ToString())) 
            {
                CommentMessages.NoMore();
                return messages;
            }

            var result = await ApiService.Instance.GetCommentMessages(timeStamp);
            if (result != null && result.CommentMessages != null)
            {
                CommentMessages.TimeStamp = result.TimeStamp;

                commentMessageStringBuilder.Clear();
                commentMessageStringBuilder.Append(timeStamp);
                
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

        private StringBuilder voteMessageTimeStamp = new StringBuilder("0");

        public async Task<IEnumerable<Message>> LoadVoteMessage(uint count, string timeStamp)
        { 
            List<Message> messages = new List<Message>();

            if (timeStamp.Equals(voteMessageTimeStamp))
            {
                VoteMessages.NoMore();
                return messages;
            }

            var result = await ApiService.Instance.GetCommentVoteMessages(timeStamp);
            if (result != null && result.CommentMessages != null)
            {
                VoteMessages.TimeStamp = result.TimeStamp;

                voteMessageTimeStamp.Clear();
                voteMessageTimeStamp.Append(result.TimeStamp);

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

        private StringBuilder adminMessageTimeStamp = new StringBuilder("0");

        public async Task<IEnumerable<Message>> LoadAdminMessage(uint count, string timeStamp)
        { 
            List<Message> messages = new List<Message>();

            if (timeStamp.Equals(adminMessageTimeStamp))
            {
                AdminMessages.NoMore();
                return messages;
            } 

            var result = await ApiService.Instance.GetAdminMessages(timeStamp);
            if (result != null && result.AdminMessages != null)
            {
                CommentMessages.TimeStamp = result.TimeStamp;

                adminMessageTimeStamp.Clear();
                adminMessageTimeStamp.Append(result.TimeStamp);
                
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

        #region Refresh methods
        /// <summary>
        /// 刷新评论消息
        /// </summary>
        public async void RefreshCommentMessages()
        {
            IsActive = true;
            await CommentMessages.ClearAndReloadAsync();
            IsActive = false;
        }

        /// <summary>
        /// 刷新赞消息
        /// </summary>
        public async void RefreshVoteMessages()
        {
            IsActive = true;
            await VoteMessages.ClearAndReloadAsync();
            IsActive = false;
        }

        /// <summary>
        /// 刷新系统消息
        /// </summary>
        public async void RefreshAdminMessages()
        {
            IsActive = true;
            await AdminMessages.ClearAndReloadAsync();
            IsActive = false;
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
