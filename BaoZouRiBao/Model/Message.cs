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
        
        /// <summary>
        /// 时间戳
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedTime { get; set; }
        
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// 正常格式的时间 YYYY-mm-dd hh:mm
        /// </summary>
        [JsonProperty(PropertyName = "sended_at")]
        public string SendTime { get; set; }

        /// <summary>
        /// 发送者信息
        /// </summary>
        [JsonProperty(PropertyName = "sender")]
        public Recommender Sender { get; set; }
        
        /// <summary>
        /// 接收者信息
        /// </summary>
        [JsonProperty(PropertyName = "receiver")]
        public Recommender Receiver { get; set; }

        [JsonIgnore]
        [JsonProperty(PropertyName = "vote")]
        public Vote Vote { get; set; }

        [JsonIgnore]
        [JsonProperty(PropertyName = "voted_comment")]
        public Comment VotedComment { get; set; }

        [JsonProperty(PropertyName = "sender_comment")]
        public Comment SenderComment { get; set; }

        [JsonProperty(PropertyName = "receiver_comment")]
        public Comment ReceiverComment { get; set; }
    }
}
