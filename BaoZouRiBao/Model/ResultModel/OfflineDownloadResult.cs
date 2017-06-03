using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    /// <summary>
    /// 离线下载实体类
    /// </summary>
    public class OfflineDownloadResult
    {
        /// <summary>
        /// 新闻列表
        /// </summary>
        [JsonProperty(PropertyName = "newslist")]
        public List<News> News { get; set; }

        /// <summary>
        /// Html header
        /// </summary>
        [JsonProperty(PropertyName = "head_replace")]
        public List<HtmlHead> Heads{ get; set; }

    }

    public class ArticlesItem
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "image_source")]
        public string ImageSource { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "video_file_url")]
        public string VideoFileUrl { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        [JsonProperty(PropertyName = "thumbnail")]
        public string Thumbnail { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [JsonProperty(PropertyName = "author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "share_image")]
        public string ShareImage { get; set; }

        /// <summary>
        /// 努力的doge 兄弟你还是洗洗睡吧 暴走天天看
        /// </summary>
        [JsonProperty(PropertyName = "key_words")]
        public string KeyWords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "video_image_url")]
        public string VideoImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "author_avatar")]
        public string AuthorAvatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "document_id")]
        public int DocumentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "display_type")]
        public int DisplayType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "section_id")]
        public int SectionId { get; set; }

        /// <summary>
        /// 2017 年 2 月
        /// </summary>
        [JsonProperty(PropertyName = "display_date")]
        public string DisplayDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "ga_prefix")]
        public string GaPrefix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "vote_count")]
        public int VoteCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "share_url")]
        public string ShareUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "hit_count")]
        public int HitCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "hit_count_string")]
        public string HitCountString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "tag")]
        public string Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "tag_text")]
        public string TagText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "guide")]
        public string Guide { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "guide_image")]
        public string GuideImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "timestamp")]
        public string timestamp { get; set; }

        /// <summary>
        /// 一只还算努力的doge
        /// </summary>
        [JsonProperty(PropertyName = "author_summary")]
        public string AuthorSummary { get; set; }

        /// <summary>
        /// 暴走天天看
        /// </summary>
        [JsonProperty(PropertyName = "section_name")]
        public string section_name { get; set; }

        /// <summary>
        /// 暴走漫画网站每日精选！
        /// </summary>
        [JsonProperty(PropertyName = "section_description")]
        public string SectionDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "section_image")]
        public string SectionImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "section_color")]
        public string SectionColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "js")]
        public List<string> Js { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "css")]
        public List<string> Css { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "body")]
        public string body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "head")]
        public string Head { get; set; }
    }
     
}
