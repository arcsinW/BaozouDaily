using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model
{
    public class AuthenticationResult
    {
        #region Login fail
        [JsonProperty(PropertyName = "detail")]
        public string Detail { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
        #endregion

        #region Login success
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "user_avatar")]
        public string Avatar { get; set; }

        [JsonProperty(PropertyName = "user_name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "client_id")]
        public string client_id { get; set; }
        #endregion
    }
}
