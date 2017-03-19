using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
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

        //[JsonProperty(PropertyName = "voted")]
        //public string Voted { get; set; }

        [JsonProperty(PropertyName = "hack_js")]
        public HackJs HackJs { get; set; }
    }

    public class HackJs
    {
        [JsonProperty(PropertyName = "ducoment_loaded")]
        public string DocumentLoaded { get; set; }

        [JsonProperty(PropertyName = "set_day_mode")]
        public string SetDayMode { get; set; }

        [JsonProperty(PropertyName = "set_night_mode")]
        public string SetNightMode { get; set; }
    }
}
