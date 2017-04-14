using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    public class ReportCommentResult
    {
        [JsonProperty(PropertyName = "alertDesc")]
        public string AlertDesc { get; set; }

        [JsonProperty(PropertyName = "desc")]
        public string Description {get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
