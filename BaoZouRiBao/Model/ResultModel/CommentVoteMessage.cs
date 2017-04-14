using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    /// <summary>
    /// 赞 的消息
    /// </summary>
    public class CommentVoteMessage
    {
        [JsonProperty(PropertyName = "timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty(PropertyName = "unread_count")]
        public string UnReadCount { get; set; }

        [JsonProperty(PropertyName = "comment_message")]
        public string CommentMessage { get; set; }
    }
}
