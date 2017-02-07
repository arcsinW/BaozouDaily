using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model
{
    public class TaskInfo
    {
        [JsonProperty(PropertyName = "article_read_count")]
        public string ArticleReadCount { get; set; }

        [JsonProperty(PropertyName = "balance")]
        public int Balance { get; set; }

        [JsonProperty(PropertyName = "comment_count")]
        public string CommentCount { get; set; }

        [JsonProperty(PropertyName = "contribute_count")]
        public string ContributeCount { get; set; }

        [JsonProperty(PropertyName = "daily_tasks")]
        public List<TaskItem> DailyTasks { get; set; }

        [JsonProperty(PropertyName = "favorite_count")]
        public string FavoriteCount { get; set; }
    }

    public class TaskItem
    {
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }

        [JsonProperty(PropertyName = "done")]
        public string Done { get; set; }

        [JsonProperty(PropertyName = "increase")]
        public string Increase { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "task_amount")]
        public string TaskAmount { get; set; }

        [JsonProperty(PropertyName = "task_amount_max")]
        public string TaskAmountMax { get; set; }

        [JsonProperty(PropertyName = "task_amount_min")]
        public string TaskAmountMin { get; set; }

        [JsonProperty(PropertyName = "task_id")]
        public string TaskId { get; set; }

     
    }
}
