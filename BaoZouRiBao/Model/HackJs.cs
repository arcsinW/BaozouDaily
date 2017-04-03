using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    public class HackJs
    {
        [JsonProperty(PropertyName = "ducoment_loaded")]
        public string DocumentLoaded { get; set; }

        [JsonProperty(PropertyName = "set_day_mode")]
        public string SetDayMode { get; set; }

        [JsonProperty(PropertyName = "set_night_mode")]
        public string SetNightMode { get; set; }
    }
}
