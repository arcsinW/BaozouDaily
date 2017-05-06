using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    /// <summary>
    /// 系统通知 消息
    /// </summary>
    public class AdminMessageResult
    {
        [JsonProperty(PropertyName = "timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty(PropertyName = "unread_count")]
        public string UnReadCount { get; set; }

        [JsonProperty(PropertyName = "admin_messages")]
        public List<Message> AdminMessages { get; set; }
    }
}
