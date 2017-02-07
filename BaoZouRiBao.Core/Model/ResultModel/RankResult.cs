using BaoZouRiBao.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.ResultModel
{
    public class RankResult
    {
        [JsonProperty(PropertyName = "data")]
        public Document[] Documents { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public string TimeStamp { get; set; }
    }
}
