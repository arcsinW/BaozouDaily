using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Http
{
    public class ServiceUri
    {
        #region Need Authentication
        public const string OAuth2 = "http://api.ibaozou.com/oauth2/server/accesstoken";

        public const string AccessToken = "https://api.weibo.com/oauth2/access_token";

        public const string Login = "http://dailyapi.ibaozou.com/api/v31/user/login";

        /// <summary>
        /// Post 
        /// </summary>
        public const string LogOut = "http://dailyapi.ibaozou.com/api/v31/user/logout";

        public const string TaskInfo = "http://dailyapi.ibaozou.com/api/v31/task/info";

        public const string MessagesCount = "http://dailyapi.ibaozou.com/api/v31/messages/count";

        public const string UserAvatar = "http://dailyapi.ibaozou.com/api/v1/user/avatar";

        /// <summary>
        /// {0} AccessToken
        /// </summary>
        public const string MyCoins = "http://dailyapi.ibaozou.com/coin?token={0}";

        public const string MyFavorite = "http://dailyapi.ibaozou.com/api/v31/documents/favorites?timestamp={0}&";

        public const string MyComment = "http://dailyapi.ibaozou.com/api/v31/comments/my?timestamp={0}&";

        public const string MyReadHistory = "http://dailyapi.ibaozou.com/api/v31/documents/read?timestamp={0}&";

        public const string MyContribute = "http://dailyapi.ibaozou.com/api/v31/documents/my_contribute?timestamp={0}&";

        public const string ClearReadHistory = "http://dailyapi.ibaozou.com/api/v31/documents/read/clear";

        /// <summary>
        /// Like a comment
        /// {0} comment id 3325434
        /// </summary>
        public const string LikeComment = "http://dailyapi.ibaozou.com/api/v31/comments/{0}/like";
        
        /// <summary>
        /// POST image
        /// </summary>
        public const string UploadAvatar = "http://dailyapi.ibaozou.com/api/v1/user/avatar";
        #endregion

        public const string LatestDocument = "http://dailyapi.ibaozou.com/api/v31/documents/latest?timestamp={0}";

        public const string Document = "http://dailyapi.ibaozou.com/api/v31/documents/{0}";

        public const string DocumentExtra = "http://dailyapi.ibaozou.com/api/v31/documents/{0}/extra";

        public const string DocumentRelated = "http://dailyapi.ibaozou.com/api/v31/documents/{0}/related";

        /// <summary>
        /// Hot or latest comments in documents  GET
        /// Write a comment to a document           POST   (postData  : {"content" : "吐槽"}
        /// </summary>
        public const string DocumentComments = "http://dailyapi.ibaozou.com/api/v31/documents/{0}/comments";

        /// <summary>
        /// {0} documentId
        /// {1} type hot latest
        /// {2} timestamp
        /// </summary>
        public const string LatestOrHotComment = "http://dailyapi.ibaozou.com/api/v31/documents/{0}/comments/{1}?timestamp={2}&";
        
        public const string LatestContribute = "http://dailyapi.ibaozou.com/api/v31/documents/contributes/latest?timestamp={0}";

        public const string LatestVideo = "http://dailyapi.ibaozou.com/api/v31/documents/videos/latest?timestamp={0}";
         
        /// <summary>
        /// {0} read vote comment
        /// {1} day week month
        /// </summary>
        public const string Rank = "http://dailyapi.ibaozou.com/api/v31/rank/{0}/{1}";
         
        public const string Channels = "http://dailyapi.ibaozou.com/api/v31/channels/index?page={0}&per_page={1}&";

        public const string ContributeInChannel = "http://dailyapi.ibaozou.com/api/v31/channels/{0}?timestamp={1}&";

        public const string TaskDone = "http://dailyapi.ibaozou.com/api/v31/task/done";

        public const string Favorite  = "http://dailyapi.ibaozou.com/api/v31/documents/{0}/favorite";

        public const string Search = "http://dailyapi.ibaozou.com/api/v31/documents/search";

        public const string Vote = "http://dailyapi.ibaozou.com/api/v31/documents/{0}/vote";
        

        public const string FAQ = "http://dailyapi.ibaozou.com/faq?category_id=1";

        public const string DocumentOfflineDownload = "http://dailyapi.ibaozou.com/api/v31/documents/android_offline_download";
    }
}
