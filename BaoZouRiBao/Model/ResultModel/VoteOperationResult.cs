using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    /// <summary>
    /// 点赞操作返回值
    /// </summary>
    public class VoteOperationResult
    {
        [JsonProperty(PropertyName = "alertDesc")]
        public string alertDesc { get; set; }
        
        [JsonProperty(PropertyName = "desc")]
        public string Description { get; set; }
        
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "data")]
        public VoteOperationData Data { get; set; }
    }

    public class VoteOperationData
    {
        [JsonProperty(PropertyName = "count")]
        public string Count { get; set; }
    }
}
