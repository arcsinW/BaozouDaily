using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model
{
    public class DocumentRelated
    {
        [JsonProperty(PropertyName = "data")]
        public Document[] RelatedStories { get; set; }
    }
}
