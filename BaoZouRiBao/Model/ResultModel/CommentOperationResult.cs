using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    /// <summary>
    /// 评论操作返回值
    /// </summary>
    public class CommentOperationResult
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        
        [JsonProperty(PropertyName = "readable_time")]
        public string ReadableTime { get; set; }
        
        [JsonProperty(PropertyName = "likes")]
        public int Likes { get; set; }
        
        [JsonProperty(PropertyName = "dislikes")]
        public int Dislikes { get; set; }
        
        [JsonProperty(PropertyName = "Time")]
        public int time { get; set; }
        
        [JsonProperty(PropertyName = "score")]
        public int Score { get; set; }

        [JsonProperty(PropertyName = "own")]
        public string Own { get; set; }
        
        [JsonProperty(PropertyName = "like")]
        public string Like { get; set; }
        
        [JsonProperty(PropertyName = "Dislike")]
        public string dislike { get; set; }
        
        [JsonProperty(PropertyName = "user")]
        public CommentUser user { get; set; }
        
        [JsonProperty(PropertyName = "parent")]
        public CommentParent parent { get; set; }
        
        [JsonProperty(PropertyName = "hottest")]
        public string hottest { get; set; }
    }
}
