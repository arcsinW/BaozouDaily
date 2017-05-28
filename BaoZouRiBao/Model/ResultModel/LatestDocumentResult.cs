using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    public class LatestDocumentResult
    {
        [JsonProperty(PropertyName = "timestamp")]
        public string TimeStamp;

        [JsonProperty(PropertyName = "data")]
        public List<Document> Data { get; set; }

        [JsonProperty(PropertyName = "top_stories")]
        public List<Document> TopStories { get; set; }
    } 
}
