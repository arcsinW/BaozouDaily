using BaoZouRiBao.Controls;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class MessagePageViewModel : ViewModelBase
    {
        public MessagePageViewModel()
        {
            if (IsDesignMode)
            {
                //LoadDesignData();
            }

            CommentMessages = new IncrementalLoadingList<Message>(LoadCommentMessages, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { IsActive = false; ToastService.SendToast(e.Message); });
            VoteMessages = new IncrementalLoadingList<Message>(LoadVoteMessage, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { IsActive = false; ToastService.SendToast(e.Message); });
            AdminMessages = new IncrementalLoadingList<Message>(LoadAdminMessage, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { IsActive = false; ToastService.SendToast(e.Message); });
        }

        /// <summary>
        /// 加载设计时数据
        /// </summary>
        private void LoadDesignData()
        {
            string commentJson = "{\"comment_messages\":[{\"id\":3984488,\"content\":\"\",\"created_at\":1494394824,\"sended_at\":\"2017-05-10 13:40\",\"sender\":{\"name\":\"X_arcsinw\",\"avatar\":\"http://tva2.sinaimg.cn/crop.0.0.480.480.1024/e5b9045djw8ea3nvwosj3j20dc0dcaam.jpg\",\"id\":727538,\"type\":\"User\"},\"receiver\":{\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\",\"id\":720554,\"type\":\"User\"},\"sender_comment\":{\"id\":3486414,\"content\":\"哎\",\"created_at\":\"2017-05-10 13:40\",\"voted\":false,\"voted_count\":0,\"article\":{\"document_id\":6954788,\"title\":\"警察叔叔我捡到五块钱\",\"display_type\":1,\"image\":\"http://wx4.sinaimg.cn/large/006HyqNRly1ffcz79py00j303c03c3yj.jpg\",\"visiable\":true},\"user\":{\"id\":727538,\"name\":\"X_arcsinw\",\"avatar\":\"http://tva2.sinaimg.cn/crop.0.0.480.480.1024/e5b9045djw8ea3nvwosj3j20dc0dcaam.jpg\"}},\"receiver_comment\":{\"id\":3484647,\"content\":\"哎\",\"voted\":false,\"voted_count\":2,\"created_at\":\"2017-05-08 15:52\",\"article\":{\"document_id\":46251,\"title\":\"警察叔叔我捡到五块钱\",\"display_type\":1,\"image\":\"http://wx4.sinaimg.cn/large/006HyqNRly1ffcz79py00j303c03c3yj.jpg\",\"visiable\":true},\"parent\":{\"id\":3484188,\"content\":\"可能是最近周末吧\",\"created_at\":\"2017-05-07 23:43\",\"voted\":true,\"voted_count\":1,\"article\":{\"document_id\":46251,\"title\":\"警察叔叔我捡到五块钱\",\"display_type\":1,\"image\":\"http://wx4.sinaimg.cn/large/006HyqNRly1ffcz79py00j303c03c3yj.jpg\",\"visiable\":true}}}},{\"id\":3984487,\"content\":\"\",\"created_at\":1494394824,\"sended_at\":\"2017-05-10 13:40\",\"sender\":{\"name\":\"X_arcsinw\",\"avatar\":\"http://tva2.sinaimg.cn/crop.0.0.480.480.1024/e5b9045djw8ea3nvwosj3j20dc0dcaam.jpg\",\"id\":727538,\"type\":\"User\"},\"receiver\":{\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\",\"id\":720554,\"type\":\"User\"},\"sender_comment\":{\"id\":3486413,\"content\":\"哎\",\"created_at\":\"2017-05-10 13:40\",\"voted\":false,\"voted_count\":1,\"article\":{\"document_id\":6954788,\"title\":\"警察叔叔我捡到五块钱\",\"display_type\":1,\"image\":\"http://wx4.sinaimg.cn/large/006HyqNRly1ffcz79py00j303c03c3yj.jpg\",\"visiable\":true},\"user\":{\"id\":727538,\"name\":\"X_arcsinw\",\"avatar\":\"http://tva2.sinaimg.cn/crop.0.0.480.480.1024/e5b9045djw8ea3nvwosj3j20dc0dcaam.jpg\"}},\"receiver_comment\":{\"id\":3484647,\"content\":\"哎\",\"voted\":false,\"voted_count\":2,\"created_at\":\"2017-05-08 15:52\",\"article\":{\"document_id\":46251,\"title\":\"警察叔叔我捡到五块钱\",\"display_type\":1,\"image\":\"http://wx4.sinaimg.cn/large/006HyqNRly1ffcz79py00j303c03c3yj.jpg\",\"visiable\":true},\"parent\":{\"id\":3484188,\"content\":\"可能是最近周末吧\",\"created_at\":\"2017-05-07 23:43\",\"voted\":true,\"voted_count\":1,\"article\":{\"document_id\":46251,\"title\":\"警察叔叔我捡到五块钱\",\"display_type\":1,\"image\":\"http://wx4.sinaimg.cn/large/006HyqNRly1ffcz79py00j303c03c3yj.jpg\",\"visiable\":true}}}},{\"id\":3982130,\"content\":\"\",\"created_at\":1494229930,\"sended_at\":\"2017-05-08 15:52\",\"sender\":{\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\",\"id\":720554,\"type\":\"User\"},\"receiver\":{\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\",\"id\":720554,\"type\":\"User\"},\"sender_comment\":{\"id\":3484647,\"content\":\"哎\",\"created_at\":\"2017-05-08 15:52\",\"voted\":false,\"voted_count\":2,\"article\":{\"document_id\":6954788,\"title\":\"警察叔叔我捡到五块钱\",\"display_type\":1,\"image\":\"http://wx4.sinaimg.cn/large/006HyqNRly1ffcz79py00j303c03c3yj.jpg\",\"visiable\":true},\"user\":{\"id\":720554,\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\"}},\"receiver_comment\":{\"id\":3484188,\"content\":\"可能是最近周末吧\",\"voted\":true,\"voted_count\":1,\"created_at\":\"2017-05-07 23:43\",\"article\":{\"document_id\":46251,\"title\":\"警察叔叔我捡到五块钱\",\"display_type\":1,\"image\":\"http://wx4.sinaimg.cn/large/006HyqNRly1ffcz79py00j303c03c3yj.jpg\",\"visiable\":true}}},{\"id\":3981901,\"content\":\"\",\"created_at\":1494216980,\"sended_at\":\"2017-05-08 12:16\",\"sender\":{\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\",\"id\":720554,\"type\":\"User\"},\"receiver\":{\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\",\"id\":720554,\"type\":\"User\"},\"sender_comment\":{\"id\":3484525,\"content\":\"x\",\"created_at\":\"2017-05-08 12:16\",\"voted\":false,\"voted_count\":0,\"article\":{\"document_id\":6954791,\"title\":\"你为什么对我还有隐瞒\",\"display_type\":1,\"image\":\"http://wx1.sinaimg.cn/large/0066bNNyly1ffdq2zfrlfj303c03cq3f.jpg\",\"visiable\":true},\"user\":{\"id\":720554,\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\"}},\"receiver_comment\":{\"id\":3484432,\"content\":\"第一\",\"voted\":false,\"voted_count\":1,\"created_at\":\"2017-05-08 10:00\",\"article\":{\"document_id\":46258,\"title\":\"你为什么对我还有隐瞒\",\"display_type\":1,\"image\":\"http://wx1.sinaimg.cn/large/0066bNNyly1ffdq2zfrlfj303c03cq3f.jpg\",\"visiable\":true}}}],\"unread_count\":0,\"timestamp\":1494216980}";
            string voteJson = "{\"comment_vote_messages\":[{\"id\":4009868,\"content\":\"\",\"created_at\":1496036158,\"sended_at\":\"2017-05-29 13:35\",\"sender\":{\"name\":\"X_arcsinw\",\"avatar\":\"http://tva2.sinaimg.cn/crop.0.0.480.480.1024/e5b9045djw8ea3nvwosj3j20dc0dcaam.jpg\",\"id\":727538,\"type\":\"User\"},\"receiver\":{\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\",\"id\":720554,\"type\":\"User\"},\"vote\":{\"vote_id\":7284066,\"vote_content\":\"赞了你\",\"vote_at\":\"2017-05-29 13:35\"},\"voted_comment\":{\"id\":3502216,\"content\":\"？？？\",\"created_at\":\"2017-05-29 13:35\",\"voted\":false,\"voted_count\":1,\"article\":{\"document_id\":46804,\"title\":\"发人深省动画短片《然后》\",\"display_type\":3,\"image\":\"http://bsyimg2.cdn.krcom.cn/stream/X5I-7VR6mXTh0CWgdnI4~Xg36uQ6GDTt_40n0.jpg\",\"visiable\":true},\"user\":{\"id\":720554,\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\"}}}],\"unread_count\":0,\"timestamp\":1496036158}";

            var commentResult = JsonHelper.Deserlialize<CommentMessageResult>(commentJson);
            foreach (var item in commentResult.CommentMessages)
            { 
                CommentMessages.Add(item);
            }

            var voteResult = JsonHelper.Deserlialize<CommentMessageResult>(voteJson);
            foreach (var item in voteResult.CommentMessages)
            {
                VoteMessages.Add(item);
            }
        }

        #region Load data methods
        private StringBuilder commentMessageStringBuilder = new StringBuilder("0");

        /// <summary>
        /// 获取评论消息
        /// </summary>
        /// <param name="count"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> LoadCommentMessages(uint count, string timeStamp)
        { 
            if (timeStamp.Equals(commentMessageStringBuilder.ToString()))
            {
                CommentMessages.NoMore();
                return null;
            }

            List<Message> messages = new List<Message>();

            var result = await ApiService.Instance.GetCommentMessages(timeStamp);
            if (result != null && result.CommentMessages != null)
            {
                CommentMessages.TimeStamp = result.TimeStamp; 
                commentMessageStringBuilder.Clear();
                commentMessageStringBuilder.Append(timeStamp);

                result.CommentMessages?.ForEach(x => messages.Add(x));

                return messages; 
            }
            else
            {
                CommentMessages.NoMore();
            }


            if (messages.Count == 0 && CommentMessages.Count == 0)
            {
                IsCommentMessageEmpty = true;
            }
            else
            {
                IsCommentMessageEmpty = false;
            }
            return null;
        }

        private StringBuilder voteMessageTimeStamp = new StringBuilder("0");

        /// <summary>
        /// 获取点赞消息
        /// </summary>
        /// <param name="count"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> LoadVoteMessage(uint count, string timeStamp)
        {
            List<Message> messages = new List<Message>();

            if (timeStamp.Equals(voteMessageTimeStamp.ToString()))
            {
                VoteMessages.NoMore();
                return messages;
            }

            var result = await ApiService.Instance.GetCommentVoteMessages(timeStamp);
            if (result != null && result.CommentVoteMessages != null)
            {
                VoteMessages.TimeStamp = result.TimeStamp;

                voteMessageTimeStamp.Clear();
                voteMessageTimeStamp.Append(result.TimeStamp);

                foreach (var item in result.CommentVoteMessages)
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

        /// <summary>
        /// 获取系统消息
        /// </summary>
        /// <param name="count"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Message>> LoadAdminMessage(uint count, string timeStamp)
        {
            List<Message> messages = new List<Message>();

            if (timeStamp.Equals(adminMessageTimeStamp.ToString()))
            {
                AdminMessages.NoMore();
                return messages;
            } 

            var result = await ApiService.Instance.GetAdminMessages(timeStamp);
            if (result != null && result.AdminMessages != null)
            {
                AdminMessages.TimeStamp = result.TimeStamp;

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
        public async Task RefreshCommentMessagesAsync()
        {
            IsActive = true;
            commentMessageStringBuilder.Clear();
            commentMessageStringBuilder.Append("0");
            await CommentMessages.ClearAndReloadAsync();
            IsActive = false;
        }

        /// <summary>
        /// 刷新赞消息
        /// </summary>
        public async Task RefreshVoteMessagesAsync()
        {
            IsActive = true;
            voteMessageTimeStamp.Clear();
            voteMessageTimeStamp.Append("0");
            await VoteMessages.ClearAndReloadAsync();
            IsActive = false;
        }

        /// <summary>
        /// 刷新系统消息
        /// </summary>
        public async Task RefreshAdminMessagesAsync()
        {
            IsActive = true;
            adminMessageTimeStamp.Clear();
            adminMessageTimeStamp.Append("0");
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
