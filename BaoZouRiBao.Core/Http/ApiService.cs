using BaoZouRiBao.Core.Enums;
using BaoZouRiBao.Core.Helper;
using BaoZouRiBao.Core.Model;
using BaoZouRiBao.Core.Model.PostModel;
using BaoZouRiBao.Core.Model.ResultModel;
using BaoZouRiBao.Core.ResultModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace BaoZouRiBao.Core.Http
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
        public async Task<AuthenticationResult> BaoZouOAuth(string userName,string password)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("x_auth_mode", "client_auth");
            dic.Add("password", password);
            dic.Add("username", userName);
            //dic.Add("client_id", "10230202?");
            dic.Add("client_id", DeviceInformationHelper.GetDeviceId());

            var result = await PostDicForLogin(ServiceUri.OAuth2, dic);
            return result;
        }

        public async Task SinaWeiboLogin()
        {
            string result = await AuthenticationHelper.SinaAuthentication();
           
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

                    User user = await PostJson<LoginPost, User>(ServiceUri.Login, loginPost);
                    if (user != null)
                    {
                        HttpBaseService.AddHeader("Authorization", "Bearer " + user.AccessToken);
                        GlobalValue.Current.UpdateUser(user);
                    }
                }
            }
        }

        public async Task TecentLogin()
        {
            var result = await AuthenticationHelper.TencentAuthentication();
             
            LoginPost loginPost = new LoginPost()
            {

            };
            User user = await PostJson<LoginPost, User>(ServiceUri.Login, loginPost);
            if (user != null)
            {
                HttpBaseService.AddHeader("Authorization", "Bearer " + user.AccessToken);
                GlobalValue.Current.UpdateUser(user);
            }
        }

        public async Task Login(string userName,string password)
        { 
            var result = await BaoZouOAuth(userName, password);
            if (result != null && string.IsNullOrEmpty(result.Error))
            { 
                LoginPost loginPost = new LoginPost()
                {
                    AccessToken = result.AccessToken,
                    Source = "baozou",
                    User = result.UserId
                };
                User user = await PostJson<LoginPost, User>(ServiceUri.Login, loginPost);
                if (user != null)
                {
                    HttpBaseService.AddHeader("Authorization", "Bearer " + user.AccessToken);
                    GlobalValue.Current.UpdateUser(user); 
                }
            }
        }

        public async Task<bool> Logout()
        {
            string res = await PostJson<string>(ServiceUri.LogOut, "");
            if (res.Contains("success"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        
        #region Should authentication before call
        /// <summary>
        /// get daily task info
        /// </summary>
        public async Task<TaskInfo> GetTaskInfo()
        { 
            try
            {
                return await GetJson<TaskInfo>(ServiceUri.TaskInfo);
                //string result = await httpClient.GetStringAsync(new Uri(ServiceUri.TaskInfo));
                //return JsonHelper.Deserlialize<TaskInfo>(result);
            }
            catch(Exception e)
            {
                LogHelper.WriteLine(e);
                return null;
            }
        }

        public async Task<TaskDoneResult> TaskDone(string taskId)
        {
            string post = "\"task_id\": \"" + taskId + "\"}";
            var result = await PostJson<TaskDoneResult>(ServiceUri.TaskDone, post);
            return result;
        }

        public async Task<RankResult> GetMyFavorite(string timeStamp)
        {
            var uri = string.Format(ServiceUri.MyFavorite, timeStamp);
            var result = await GetJson<RankResult>(uri);
            return result;
        }

        public async Task<CommentResult> GetMyComments(string timeStamp)
        {
            var uri = string.Format(ServiceUri.MyComment, timeStamp);
            var result = await GetJson<CommentResult>(uri);
            return result;
        }

        public async Task<RankResult> GetMyReadHistory(string timeStamp)
        {
            var uri = string.Format(ServiceUri.MyReadHistory, timeStamp);
            var result = await GetJson<RankResult>(uri);
            return result;
        }

        public async Task<OperationResult> ClearReadHistory()
        {
            var result = await PostJson<OperationResult>(ServiceUri.ClearReadHistory,"");
            return result;
        }
        
        public async Task Vote(string documentId)
        {
            string url = string.Format(ServiceUri.Vote, documentId);
            TaskDoneResult result = await PostJson<TaskDoneResult>(url, "");
        }

        /// <summary>
        /// comment a document
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task Comment(string documentId, string content)
        {
            string url = string.Format(ServiceUri.DocumentComments, documentId);
            CommentResult result = await PostJson<CommentResult>(url, "{ \"conten\" : \"" + content + "\"}");
        }
#endregion

        /// <summary>
        /// MainPage
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public async Task<LatestDocumentResult> GetLatestDocument(string timestamp)
        {
            string url = string.Format(ServiceUri.LatestDocument, timestamp);
            var stories = await GetJson<LatestDocumentResult>(url);
            return stories;
        }

        public async Task<Document> GetDocument(string documentId)
        {
            string url = string.Format(ServiceUri.Document, documentId);
            var extra = await GetJson<Document>(url);
            return extra;
        }

        public async Task<Video> GetVideo(string documentId)
        {
            string url = string.Format(ServiceUri.Document, documentId);
            var video = await GetJson<Video>(url);
            return video;
        }

        public async Task<DocumentExtra> GetDocumentExtra(string documentId)
        {
            string url = string.Format(ServiceUri.DocumentExtra, documentId);
            var extra = await GetJson<DocumentExtra>(url);
            return extra;
        }

        public async Task<DocumentRelated> GetDocumentRelated(string documentId)
        {
            string url = string.Format(ServiceUri.DocumentRelated, documentId);
            var extra = await GetJson<DocumentRelated>(url);
            return extra;
        }

        public async Task<DocumentComment> GetDocumentComment(string documentId)
        {
            string url = string.Format(ServiceUri.DocumentComments, documentId);
            var extra = await GetJson<DocumentComment>(url);
            return extra;
        }

        public async Task<LatestContributeResult> GetLatestContribute(string timeStamp)
        {
            string url = string.Format(ServiceUri.LatestContribute, timeStamp);
            var stories = await GetJson<LatestContributeResult>(url);
            return stories;
        }

        public async Task<LatestVideoResult> GetLatestVideo(string timeStamp)
        {
            string url = string.Format(ServiceUri.LatestVideo, timeStamp);
            var videos = await GetJson<LatestVideoResult>(url);
            return videos;
        }
        
        public async Task<ChannelResult> GetChannels(int page,int perPage = 10)
        {
            string url = string.Format(ServiceUri.Channels, page, perPage);
            var channels = await GetJson<ChannelResult>(url);
            return channels;
        }

        public async Task<OperationResult> Favorite(string documentId)
        {
            string url = string.Format(ServiceUri.Favorite, documentId);
            var result = await PostJson<OperationResult>(url, "");
            return result;
        }

        public async Task<ContributeInChannelResult> GetContributeInChannel(string id,string timeStamp)
        {
            string url = string.Format(ServiceUri.ContributeInChannel, id,timeStamp);
            var contributes =await GetJson<ContributeInChannelResult>(url);
            return contributes;
        }

        public async Task<LatestDocumentResult> GetRank(RankTypeEnum type,RankTimeEnum time)
        {
            string url = string.Format(ServiceUri.Rank, type, time);
            var documents = await GetJson<LatestDocumentResult>(url);
            return documents;
        }

        public async Task<LatestOrHotComment> GetLatestOrHotComments(string documentId,CommentTypeEnum type, string timeStamp)
        {
            
            string url = string.Format(ServiceUri.LatestOrHotComment, documentId, type.ToString(), timeStamp);
            var comments =  await GetJson<LatestOrHotComment>(url);
            
            return comments;
        }
        
        public async Task<SearchResult> Search(string keyword, int pageIndex)
        {
            SearchPost searchPost = new SearchPost() { Keyword = keyword, PageIndex = pageIndex };
            var searchResult = await PostJson<SearchPost,SearchResult>(ServiceUri.Search, searchPost);
            return searchResult;
        }


        public async void OfflineDownload()
        {
            await GetJson(ServiceUri.DocumentOfflineDownload);
        }
    }
}
