using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model
{
    public class Comment
    {
        [JsonProperty(PropertyName = "article")]
        public Article Article { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "dislike")]
        public string Dislike { get; set; }

        [JsonProperty(PropertyName = "dislikes")]
        public string Dislikes { get; set; }

        [JsonProperty(PropertyName = "hottest")]
        public string Hottest { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "like")]
        public string Like { get; set; }

        [JsonProperty(PropertyName = "likes")]
        public string Likes { get; set; }

        [JsonProperty(PropertyName = "own")]
        public string Own { get; set; }

        [JsonProperty(PropertyName = "parent")]
        public CommentParent Parent { get; set; }

        [JsonProperty(PropertyName = "readable_time")]
        public string ReadableTime { get; set; }

        [JsonProperty(PropertyName = "score")]
        public string Score { get; set; }

        [JsonProperty(PropertyName = "time")]
        public long Time { get; set; }

        [JsonProperty(PropertyName = "user")]
        public CommentUser User { get; set; }
    }

   
}
