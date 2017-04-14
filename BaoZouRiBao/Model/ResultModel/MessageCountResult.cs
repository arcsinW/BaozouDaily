using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    /// <summary>
    /// 获取消息数量返回值
    /// </summary>
    public class MessageCountResult
    {
        [JsonProperty(PropertyName = "admin_message_count")]
        public string AdminMessageCount { get; set; }
        
        [JsonProperty(PropertyName = "comment_message_count")]
        public string CommentMessageCount { get; set; }
        
        [JsonProperty(PropertyName = "comment_vote_message_count")]
        public string CommentVoteMessageCount { get; set; }    

    }
}
