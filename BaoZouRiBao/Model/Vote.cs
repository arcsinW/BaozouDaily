using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    public class Vote
    {
        [JsonProperty(PropertyName = "vote_id")]
        public string VoteId { get; set; }

        /// <summary>
        /// 点赞内容（一般为“赞了你”）
        /// </summary>
        [JsonProperty(PropertyName = "vote_content")]
        public string VoteContent { get; set; }

        /// <summary>
        /// 点赞时间
        /// </summary>
        [JsonProperty(PropertyName = "vote_at")]
        public string VoteAt { get; set; }

    }
}
