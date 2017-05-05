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
            List<Message> messages = await ApiService.Instance.GetCommentMessages();
            return messages;
        }

        public async Task<IEnumerable<Message>> LoadVoteMessage(uint count, string timeStamp)
        {
            List<Message> messages = await ApiService.Instance.GetCommentMessages();
            return messages;
        }

        public async Task<IEnumerable<Message>> LoadAdminMessage(uint count, string timeStamp)
        {
            List<Message> messages = await ApiService.Instance.GetCommentMessages();
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

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        } 
        #endregion
    }
}
