using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model.ResultModel
{
    public class OperationResult
    {
        [JsonProperty(PropertyName = "alertDesc")]
        public string AlertDesc { get; set; }

        [JsonProperty(PropertyName = "desc")]
        public string Desc { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
