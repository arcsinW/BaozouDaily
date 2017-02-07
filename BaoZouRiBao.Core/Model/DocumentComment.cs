using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model
{
    public class DocumentComment
    {
        [JsonProperty(PropertyName = "hottest")]
        public Comment[] Hottest { get; set; }

        [JsonProperty(PropertyName ="latest")]
        public Comment[] Latest { get; set; }
    }  
}
