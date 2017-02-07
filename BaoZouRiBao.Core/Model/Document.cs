using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model
{
    /// <summary>
    /// 首页
    /// </summary>
    public class Document
    {
        [JsonProperty(PropertyName = "author_avatar")]
        public string AuthorAvatar { get; set; }

        [JsonProperty(PropertyName = "author_id")]
        public string AuthorId { get; set; }

        [JsonProperty(PropertyName = "author_name")]
        public string AuthorName { get; set; }

        [JsonProperty(PropertyName = "author_summary")]
        public string AuthorSummary { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "channels")]
        public Channel[] Channels { get; set; }

        [JsonProperty(PropertyName = "comment_count")]
        public string CommentCount { get; set; }

        [JsonProperty(PropertyName = "commented")]
        public string Commented { get; set; }

        [JsonProperty(PropertyName = "display_date")]
        public string DisplayDate { get; set; }

        [JsonProperty(PropertyName = "display_type")]
        public string DisplayType { get; set; }

        [JsonProperty(PropertyName = "document_id")]
        public string DocumentId { get; set; }

        [JsonProperty(PropertyName = "favorited")]
        public string Favorited { get; set; }

        [JsonProperty(PropertyName = "ga_prefix")]
        public string GaPrefix { get; set; }

        [JsonProperty(PropertyName = "guide")]
        public string Guide { get; set; }

        [JsonProperty(PropertyName = "guide_image")]
        public string GuideImage { get; set; }

        [JsonProperty(PropertyName = "head")]
        public string Head { get; set; }

        [JsonProperty(PropertyName = "hit_count")]
        public string HitCount { get; set; }

        [JsonProperty(PropertyName = "hit_count_string")]
        public string HitCountString { get; set; }

        [JsonProperty(PropertyName = "publish_time")]
        public string PublishTime { get; set; }

        [JsonProperty(PropertyName = "published_at")]
        public string PublishAt { get; set; }

        [JsonProperty(PropertyName = "recommenders")]
        public Recommender[] Recommenders { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "section_name")]
        public string SectionName { get; set; }

        [JsonProperty(PropertyName = "section_image")]
        public string SectionImage { get; set; }

        [JsonProperty(PropertyName = "key_words")]
        public string KeyWords { get; set; }

        [JsonProperty(PropertyName = "section_color")]
        public string SectionColor { get; set; }

        [JsonProperty(PropertyName = "section_id")]
        public string SectionId { get; set; }
        
        [JsonProperty(PropertyName = "share_url")]
        public string ShareUrl { get; set; }

        [JsonProperty(PropertyName = "original_tag")]
        public string OriginalTag { get; set; }

        [JsonProperty(PropertyName = "tag_text")]
        public string TagText { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "video_file_url")]
        public string VideoFileUrl { get; set; }

        [JsonProperty(PropertyName = "video_image_url")]
        public string VideoImageUrl { get; set; }

        [JsonProperty(PropertyName = "vote_count")]
        public string VoteCount { get; set; }

        [JsonProperty(PropertyName = "voted")]
        public string Voted { get; set; }
    }
}
