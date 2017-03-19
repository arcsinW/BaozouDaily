using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    public class Article
    {
        [JsonProperty(PropertyName = "display_type")]
        public string DisplayType { get; set; }

        [JsonProperty(PropertyName = "document_id")]
        public string DocumentId { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "visiable")]
        public string Visiable { get; set; }

    }
}
