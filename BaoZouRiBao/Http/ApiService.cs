using BaoZouRiBao.Model;
using BaoZouRiBao.Model.PostModel;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Web.Http;
using BaoZouRiBao.Enums;
using BaoZouRiBao.Helper;

namespace BaoZouRiBao.Http
{
    public class ApiService : ApiBaseService
    {
        #region Singleton
        private ApiService()
        {

        }

        private static ApiService _apiService = new ApiService();

        public static ApiService Instance
        {
            get
            {
                return _apiService;
            }
        }
        #endregion

        #region Authentication 
        /// <summary>
        /// 暴走登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<AuthenticationResult> BaoZouOAuthAsync(string userName,string password)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("x_auth_mode", "client_auth");
            dic.Add("password", password);
            dic.Add("username", userName);
            //dic.Add("client_id", "10230202?");
            dic.Add("client_id", InformationHelper.DeviceId);

            var result = await PostDicForLogin(ServiceUri.OAuth2, dic);
            return result;
        }

        /// <summary>
        /// 新浪weibo登录
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SinaWeiboLoginAsync()
        {
            string result = await AuthenticationHelper.SinaAuthenticationAsync();
           
            Regex regex = new Regex(@"(?<=code=)(.)*");
            Match match = regex.Match(result);
            if (match.Success)
            { 
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("client_id", "3341101057");
                dic.Add("client_secret", "0d0fe859e31afdc487d89fa3f3f0fe40");
                dic.Add("grant_type", "authorization_code");
                dic.Add("redirect_uri", "http://daily.ibaozou.com/sina_weibo/auth");
                dic.Add("code", match.Value);

                var acccessToken = await PostDic<AccessTokenResult>(dic, ServiceUri.AccessToken);
                if (acccessToken != null)
                {
                    LoginPost loginPost = new LoginPost()
                    {
                        AccessToken = acccessToken.AccessToken,
                        Source = "sina",
                        User = acccessToken.Uid,
                    };

                    User user = await Post<LoginPost, User>(ServiceUri.Login, loginPost);
                    if (user != null)
                    {
                        HttpBaseService.AddHeader("Authorization", "Bearer " + user.AccessToken);
                        GlobalValue.Current.UpdateUser(user);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 腾讯weibo登录
        /// </summary>
        /// <returns></returns>
        public async Task<bool> TecentLoginAsync()
        {
            var result = await AuthenticationHelper.TencentAuthenticationAsync();
             
            LoginPost loginPost = new LoginPost()
            {

            };
            User user = await Post<LoginPost, User>(ServiceUri.Login, loginPost);
            if (user != null)
            {
                HttpBaseService.AddHeader("Authorization", "Bearer " + user.AccessToken);
                GlobalValue.Current.UpdateUser(user);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 暴走账号登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> LoginAsync(string userName,string password)
        { 
            var result = await BaoZouOAuthAsync(userName, password);
            if (result != null && string.IsNullOrEmpty(result.Error))
            { 
                LoginPost loginPost = new LoginPost()
                {
                    AccessToken = result.AccessToken,
                    Source = "baozou",
                    User = result.UserId
                };

                User user = await Post<LoginPost, User>(ServiceUri.Login, loginPost);
                if (user != null)
                {
                    HttpBaseService.AddHeader("Authorization", "Bearer " + user.AccessToken);
                    GlobalValue.Current.UpdateUser(user);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LogoutAsync()
        {
            var result = await Post<LogoutResult>(ServiceUri.LogOut, "");
            if(result == null || string.IsNullOrEmpty(result.Result))
            {
                return false;
            }

            if (result.Result.Equals("success"))
            {
                GlobalValue.Current.UpdateUser(null);
                return true;
            }

            return false;
        }
        #endregion
        
        #region Should authentication before call
        /// <summary>
        /// 获取每日任务信息
        /// </summary>
        public async Task<TaskInfo> GetTaskInfoAsync()
        { 
            try
            {
                return await GetJson<TaskInfo>(ServiceUri.TaskInfo);
            }
            catch(Exception e)
            {
                LogHelper.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// 设置任务为完成
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<DailyTaskDoneResult> TaskDoneAsync(string taskId)
        {
            string post = "{\"task_id\": \"" + taskId + "\"}";
            var result = await Post<DailyTaskDoneResult>(ServiceUri.TaskDone, post);
            return result;
        }

        /// <summary>
        /// 获取我的收藏
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<RankResult> GetMyFavoriteAsync(string timeStamp)
        {
            var uri = string.Format(ServiceUri.MyFavorite, timeStamp);
            var result = await GetJson<RankResult>(uri);
            return result;
        }

        /// <summary>
        /// 获取我的评论
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<CommentResult> GetMyCommentsAsync(string timeStamp)
        {
            var uri = string.Format(ServiceUri.MyComment, timeStamp);
            var result = await GetJson<CommentResult>(uri);
            return result;
        }

        /// <summary>
        /// 获取阅读历史
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<RankResult> GetMyReadHistoryAsync(string timeStamp)
        {
            var uri = string.Format(ServiceUri.MyReadHistory, timeStamp);
            var result = await GetJson<RankResult>(uri);
            return result;
        }

        /// <summary>
        /// 清除阅读历史
        /// </summary>
        /// <returns></returns>
        public async Task<VoteOperationResult> ClearReadHistoryAsync()
        {
            var result = await Post<VoteOperationResult>(ServiceUri.ClearReadHistory,"");
            return result;
        }

        /// <summary>
        /// 获取我的投稿
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<LatestContributeResult> GetMyContributeAsync(string timeStamp)
        {
            string uri = string.Format(ServiceUri.MyContribute, timeStamp);
            var result = await GetJson<LatestContributeResult>(uri);
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public void UploadAvatarAsync()
        {

        }

        #region 消息
        /// <summary>
        /// 获取 评论 消息
        /// </summary>
        public async Task<List<Message>> GetCommentMessages()
        {
            var result = await GetJson<CommentMessageResult>(ServiceUri.CommentMessage);
            if (result != null)
            {
                return result.CommentMessage;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取 赞 消息
        /// </summary>
        public async Task<List<Message>> GetCommentVoteMessages()
        {
            var result = await GetJson<CommentVoteMessageResult>(ServiceUri.CommentVoteMessage);
            if (result != null)
            {
                return result.CommentMessage;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取 系统 消息
        /// </summary>
        /// <returns></returns>
        public async Task<List<Message>> GetAdminMessages()
        {
            var result = await GetJson<AdminMessageResult>(ServiceUri.AdminMessage);
            if (result != null)
            {
                return result.CommentMessage;
            }
            else
            {
                return null;
            }
        } 
        #endregion

        /// <summary>
        /// 为 文章 | 视频 | 投稿 | 评论 点赞
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task VoteAsync(string documentId)
        {
            string url = string.Format(ServiceUri.Vote, documentId);
            VoteOperationResult result = await Post<VoteOperationResult>(url, "");
        }

        /// <summary>
        /// 评论文章 | 视频 | 投稿 | 评论 
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task CommentAsync(string documentId, string content)
        {
            string url = string.Format(ServiceUri.DocumentComments, documentId);
            CommentOperationResult result = await Post<CommentOperationResult>(url, "{ \"conten\" : \"" + content + "\"}");
        }

        /// <summary>
        /// 用户投稿
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public async Task<UserContributeResult> UserContributeAsync(string link, string title)
        {
            string json = "{ \"title\" : \"" + title + "\", \"link\" : \"" + link + "\"}";
            var result = await Post<UserContributeResult>(ServiceUri.UserContribute, json);
            return result;
        }

        /// <summary>
        /// 获取消息数量
        /// </summary>
        public async Task GetMessageAsync()
        {
            var result = await GetJson<MessageCountResult>(ServiceUri.MessagesCount);

        }

        /// <summary>
        /// 获取token信息
        /// </summary>
        /// <returns></returns>
        public async Task GetTokenInfoAsync()
        {
            var result = await GetJson<TokenInfoResult>(ServiceUri.TokenInfo);
        }
        #endregion

        /// <summary>
        /// 举报评论
        /// </summary>
        /// <param name="commentId">评论的Id</param>
        public void ReportComment(string commentId)
        {
            var result = GetJson<ReportCommentResult>(string.Format(ServiceUri.ReportComment, commentId));
        }

        /// <summary>
        /// 获取最新的文章列表
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public async Task<LatestDocumentResult> GetLatestDocumentAsync(string timestamp)
        {
            string url = string.Format(ServiceUri.LatestDocument, timestamp);
            var stories = await GetJson<LatestDocumentResult>(url);
            return stories;
        }

        /// <summary>
        /// 通过ID获取文章
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<Document> GetDocumentAsync(string documentId)
        {
            string url = string.Format(ServiceUri.Document, documentId);
            var extra = await GetJson<Document>(url);
            return extra;
        }

        /// <summary>
        /// 通过ID获取视频
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<Video> GetVideoAsync(string documentId)
        {
            string url = string.Format(ServiceUri.Document, documentId);
            var video = await GetJson<Video>(url);
            return video;
        }
        
        /// <summary>
        /// 获取文章的额外信息
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<DocumentExtra> GetDocumentExtraAsync(string documentId)
        {
            string url = string.Format(ServiceUri.DocumentExtra, documentId);
            var extra = await GetJson<DocumentExtra>(url);
            return extra;
        }

        /// <summary>
        /// 获取相关文章
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<DocumentRelated> GetDocumentRelatedAsync(string documentId)
        {
            string url = string.Format(ServiceUri.DocumentRelated, documentId);
            var extra = await GetJson<DocumentRelated>(url);
            return extra;
        }

        /// <summary>
        /// 获取文章评论
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<DocumentComment> GetDocumentCommentAsync(string documentId)
        {
            string url = string.Format(ServiceUri.DocumentComments, documentId);
            var extra = await GetJson<DocumentComment>(url);
            return extra;
        }

        /// <summary>
        /// 获取最新投稿列表
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<LatestContributeResult> GetLatestContributeAsync(string timeStamp)
        {
            string url = string.Format(ServiceUri.LatestContribute, timeStamp);
            var stories = await GetJson<LatestContributeResult>(url);
            return stories;
        }

        /// <summary>
        /// 获取最新视频列表
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<LatestVideoResult> GetLatestVideoAsync(string timeStamp)
        {
            string url = string.Format(ServiceUri.LatestVideo, timeStamp);
            var videos = await GetJson<LatestVideoResult>(url);
            return videos;
        }
        
        /// <summary>
        /// 获取频道列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<ChannelResult> GetChannelsAsync(int page,int perPage = 10)
        {
            string url = string.Format(ServiceUri.Channels, page, perPage);
            var channels = await GetJson<ChannelResult>(url);
            return channels;
        }

        /// <summary>
        /// 收藏指定文章
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<VoteOperationResult> FavoriteAsync(string documentId)
        {
            string url = string.Format(ServiceUri.Favorite, documentId);
            var result = await Post<VoteOperationResult>(url, "");
            return result;
        }

        /// <summary>
        /// 获取频道中的投稿
        /// </summary>
        /// <param name="id"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<ContributeInChannelResult> GetContributeInChannelAsync(string id,string timeStamp)
        {
            string url = string.Format(ServiceUri.ContributeInChannel, id,timeStamp);
            var contributes =await GetJson<ContributeInChannelResult>(url);
            return contributes;
        }

        /// <summary>
        /// 获取排行榜列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task<LatestDocumentResult> GetRankAsync(RankTypeEnum type,RankTimeEnum time)
        {
            string url = string.Format(ServiceUri.Rank, type, time);
            var documents = await GetJson<LatestDocumentResult>(url);
            return documents;
        }

        /// <summary>
        /// 获取文章的最新/最热评论
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="type"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public async Task<LatestOrHotComment> GetLatestOrHotCommentsAsync(string documentId,CommentTypeEnum type, string timeStamp)
        {
            string url = string.Format(ServiceUri.LatestOrHotComment, documentId, type.ToString(), timeStamp);
            var comments =  await GetJson<LatestOrHotComment>(url);   
            return comments;
        }

        /// <summary>
        /// 按关键字搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<SearchResult> SearchAsync(string keyword, int pageIndex = 1)
        {
            SearchPost searchPost = new SearchPost() { Keyword = keyword, PageIndex = pageIndex };
            var searchResult = await Post<SearchPost, SearchResult>(ServiceUri.Search, searchPost);
            return searchResult;
        }

        /// <summary>
        /// 离线下载
        /// </summary>
        public async Task OfflineDownloadAsync()
        {
            var result = await GetJson<string>(ServiceUri.DocumentOfflineDownload);
        }
    }
}
