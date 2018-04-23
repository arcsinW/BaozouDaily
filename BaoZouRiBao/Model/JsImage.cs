using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    public class JsImage
    {

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "src")]
        public string Src { get; set; }

        [JsonProperty(PropertyName = "index")]
        public int Index { get; set; }
    }
}
