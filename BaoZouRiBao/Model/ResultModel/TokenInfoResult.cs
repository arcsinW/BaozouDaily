using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    /// <summary>
    /// /user/token_info
    /// </summary>
    public class TokenInfoResult
    {
        [JsonProperty(PropertyName = "create_time")]
        public string CreateTime { get; set; }
        
        [JsonProperty(PropertyName = "expire_time")]
        public string ExpireTime { get; set; }
        
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }
    }
}
