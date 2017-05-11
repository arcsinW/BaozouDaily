using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    public class VotedComment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "voted")]
        public bool Voted { get; set; }

        [JsonProperty(PropertyName = "voted_count")]
        public string VotedCount { get; set; }

        [JsonProperty(PropertyName = "article")]
        public Article Article { get; set; }

        [JsonProperty(PropertyName = "user")]
        public Recommender User { get; set; }

        [JsonProperty(PropertyName = "parent")]
        public Recommender Parent { get; set; }
    }
}
