using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model
{
    public class LatestDocumentResult
    {
        [JsonProperty(PropertyName = "timestamp")]
        public double TimeStamp;

        [JsonProperty(PropertyName = "data")]
        public Document[] Data { get; set; }

        [JsonProperty(PropertyName = "top_stories")]
        public Document[] TopStories { get; set; }
    } 
}
