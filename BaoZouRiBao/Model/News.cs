using BaoZouRiBao.Model.ResultModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    public class News
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "articles")]
        public List<ArticlesItem> Articles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "date")]
        public string date { get; set; }

        /// <summary>
        /// 2017.2.1 星期三
        /// </summary>
        [JsonProperty(PropertyName = "display_date")]
        public string display_date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "timestamp")]
        public string timestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "top_stories")]
        public List<ArticlesItem> top_stories { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "is_today")]
        public bool is_today { get; set; }
    }
}
