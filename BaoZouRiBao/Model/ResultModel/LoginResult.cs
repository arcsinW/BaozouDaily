using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    public class User : ModelBase
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty(PropertyName = "avatar")]
        public string Avatar { get; set; } = "/Assets/Images/avatar_m.png";

        [JsonProperty(PropertyName = "bound_services")]
        public string[] BoundServices { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = "登录";

        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }
    }
}
