using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    public class LatestContributeResult
    {
        [JsonProperty(PropertyName = "data")]
        public Contribute[] Contributes { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public string TimeStamp { get; set; }
    }
}
