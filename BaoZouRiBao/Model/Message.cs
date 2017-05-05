using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    /// <summary>
    /// 消息
    /// </summary>
    public class Message
    {
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedTime { get; set; }
        
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "sended_at")]
        public string SendTime { get; set; }

        [JsonProperty(PropertyName = "sender")]
        public Recommender Sender { get; set; }
    }
}
