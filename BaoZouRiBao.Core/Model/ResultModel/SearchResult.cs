using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model.ResultModel
{
    public class SearchResult
    {
        [JsonProperty(PropertyName = "data")]
        public Document[] Documents { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "per_page")]
        public int PerPages { get; set; }

        [JsonProperty(PropertyName = "page")]
        public int PageIndex { get; set; }

        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; set; }
    }
}
