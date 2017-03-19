using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    /// <summary>
    /// 投稿
    /// </summary>
    public class Contribute
    {
        [JsonProperty(PropertyName = "author_avatar")]
        public string AuthorAvatar { get; set; }

        [JsonProperty(PropertyName = "author_id")]
        public string AuthorId { get; set; }

        [JsonProperty(PropertyName = "author_name")]
        public string AuthorName { get; set; }

        [JsonProperty(PropertyName = "comment_count")]
        public string CommentCount { get; set; }

        [JsonProperty(PropertyName = "commented")]
        public string Commented { get; set; }

        [JsonProperty(PropertyName = "contribute")]
        public string ContributeType { get; set; }

        [JsonProperty(PropertyName = "display_type")]
        public string DisplayType{ get; set; }

        [JsonProperty(PropertyName = "document_id")]
        public string DocumentId { get; set; }

        [JsonProperty(PropertyName = "favorited")]
        public string Favorited { get; set; }

        [JsonProperty(PropertyName = "hit_count")]
        public string HitCount { get; set; }

        [JsonProperty(PropertyName = "hit_count_string")]
        public string HitCountString { get; set; }

        [JsonProperty(PropertyName = "original_tag")]
        public string OriginalTag { get; set; }

        [JsonProperty(PropertyName = "section_color")]
        public string SectionColor { get; set; }

        [JsonProperty(PropertyName = "section_id")]
        public string SectionId { get; set; }

        [JsonProperty(PropertyName = "section_image")]
        public string SectionImage { get; set; }

        [JsonProperty(PropertyName = "section_name")]
        public string SelectionName { get; set; }

        [JsonProperty(PropertyName = "share_url")]
        public string ShareUrl { get; set; }

        [JsonProperty(PropertyName = "publish_time")]
        public string PublishTime { get; set; }

        [JsonProperty(PropertyName = "published_at")]
        public string PublishedAt { get; set; }

        [JsonProperty(PropertyName = "recommenders")]
        public Recommender[] Recommenders { get; set; }

        [JsonProperty(PropertyName = "source_name")]
        public string SourceName { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "vote_count")]
        public string VoteCount { get; set; }

        [JsonProperty(PropertyName = "voted")]
        public string Voted { get; set; }
    }
    
}
