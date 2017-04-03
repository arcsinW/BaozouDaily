using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    /// <summary>
    /// 文章的额外信息
    /// </summary>
    public class DocumentExtra
    {
        [JsonProperty(PropertyName = "channels")]
        public Channel[] Channels { get; set; }

        [JsonProperty(PropertyName = "comment_count")]
        public string CommentCount { get; set; }

        [JsonProperty(PropertyName = "display_type")]
        public string DisplayType { get; set; }

        [JsonProperty(PropertyName = "document_id")]
        public string DocumentId { get; set; }

        [JsonProperty(PropertyName = "favorited")]
        public string Favorited { get; set; }

        [JsonProperty(PropertyName = "vote_count")]
        public string VoteCount { get; set; }

        /// <summary>
        /// 用于设置日夜间模式的js
        /// </summary>
        [JsonProperty(PropertyName = "hack_js")]
        public HackJs HackJs { get; set; }
    }
}
