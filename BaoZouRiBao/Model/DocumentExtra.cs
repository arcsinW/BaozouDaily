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
    public class DocumentExtra : ModelBase
    {
        [JsonProperty(PropertyName = "channels")]
        public Channel[] Channels { get; set; }

        [JsonProperty(PropertyName = "comment_count")]
        public string CommentCount { get; set; }

        [JsonProperty(PropertyName = "display_type")]
        public string DisplayType { get; set; }

        [JsonProperty(PropertyName = "document_id")]
        public string DocumentId { get; set; }

        private bool favorited = false;
        [JsonProperty(PropertyName = "favorited")]
        public bool Favorited
        {
            get
            {
                return favorited;
            }
            set
            {
                favorited = value;
                OnPropertyChanged();
            }
        }

        private bool voted = false;

        /// <summary>
        /// 是否已点赞
        /// </summary>
        [JsonProperty(PropertyName = "voted")]
        public bool Voted
        {
            get { return voted;  }
            set { voted = value; OnPropertyChanged(); }
        }


        private string voteCount = "0";
        [JsonProperty(PropertyName = "vote_count")]
        public string VoteCount
        {
            get { return voteCount; }
            set { voteCount = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 用于设置日夜间模式的js
        /// </summary>
        [JsonProperty(PropertyName = "hack_js")]
        public HackJs HackJs { get; set; }
    }
}
