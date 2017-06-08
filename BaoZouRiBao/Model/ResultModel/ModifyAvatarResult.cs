using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model.ResultModel
{
    public class ModifyAvatarResult
    {
        [JsonProperty(PropertyName = "avatar")]
        public string Avatar { get; set; }
    }
}
