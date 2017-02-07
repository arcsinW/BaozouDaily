using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model
{
    public class LatestVideoResult
    {
        [JsonProperty(PropertyName = "data")]
        public Video[] Videos { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public string TimeStamp { get; set; }
    }

    public class Video
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

        [JsonProperty(PropertyName = "file_url")]
        public string FileUrl { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "play_count")]
        public string PlayCount { get; set; }

        [JsonProperty(PropertyName = "play_count_string")]
        public string PlayCountString { get; set; }

        [JsonProperty(PropertyName = "play_time")]
        public string PlayTime { get; set; }

        [JsonProperty(PropertyName = "publish_time")]
        public string PublishTime { get; set; }

        [JsonProperty(PropertyName = "share_url")]
        public string ShareUrl { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "vote_count")]
        public string VoteCount { get; set; }

        [JsonProperty(PropertyName = "voted")]
        public string Voted { get; set; }
        
    }
}
