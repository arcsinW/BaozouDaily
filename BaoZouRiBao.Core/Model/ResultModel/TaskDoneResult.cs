using BaoZouRiBao.Core.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model.ResultModel
{
    /// <summary>
    /// Daily task result model
    /// </summary>
    public class TaskDoneResult
    {
        [JsonProperty(PropertyName = "alertDesc")]
        public string AlertDesc { get; set; }

        [JsonProperty(PropertyName = "data")]
        public TaskDone Task { get; set; }
        
        [JsonProperty(PropertyName = "desc")]
        public string Desc { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }

    public class TaskDone
    {
        /// <summary>
        /// Count of coins got this time
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Count of total coins
        /// </summary>
        [JsonProperty(PropertyName = "balance")]
        public string Balance { get; set; }

        [JsonProperty(PropertyName = "increase")]
        public string Increase { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "task_id")]
        public BaoZouTaskEnum TaskType { get; set; }

        [JsonProperty(PropertyName = "count")]
        public string Count { get; set; }
    }
}
