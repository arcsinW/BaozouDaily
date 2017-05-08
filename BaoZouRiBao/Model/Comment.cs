using BaoZouRiBao.Common;
using BaoZouRiBao.Controls;
using BaoZouRiBao.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment : ModelBase
    {
        #region Properties
        [JsonProperty(PropertyName = "article")]
        public Article Article { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "dislike")]
        public bool Dislike { get; set; }

        private int dislikes = 0;
        [JsonProperty(PropertyName = "dislikes")]
        public int Dislikes
        {
            get
            {
                return dislikes;
            }
            set
            {
                dislikes = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(PropertyName = "hottest")]
        public string Hottest { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        private bool like = false;
        [JsonProperty(PropertyName = "like")]
        public bool Like
        {
            get
            {
                return like;
            }
            set
            {
                like = value;
                OnPropertyChanged();
            }
        }

        private int likes = 0;
        [JsonProperty(PropertyName = "likes")]
        public int Likes
        {
            get
            {
                return likes;
            }
            set
            {
                likes = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(PropertyName = "own")]
        public string Own { get; set; }

        [JsonProperty(PropertyName = "parent")]
        public CommentParent Parent { get; set; }

        [JsonProperty(PropertyName = "readable_time")]
        public string ReadableTime { get; set; }

        [JsonProperty(PropertyName = "score")]
        public string Score { get; set; }

        [JsonProperty(PropertyName = "time")]
        public long Time { get; set; }

        [JsonProperty(PropertyName = "user")]
        public CommentUser User { get; set; }
        #endregion

        /// <summary>
        /// 评论 此评论
        /// </summary>
        /// <param name="content"></param>
        public async void Reply(string content)
        {
            await ApiService.Instance.CommentAsync(Id, content);
        }
        
        public RelayCommand VoteCommand { get; set; } 

        public Comment()
        {
            VoteCommand = new RelayCommand(() => { Vote(); });
        }

        /// <summary>
        /// 点赞此评论
        /// </summary>
        public async void Vote()
        {
            var result = await ApiService.Instance.VoteCommentAsync(Id);
            if (result != null)
            {
                if (result.Status == "1000")
                {
                    Like = true;
                    Likes++;
                }
                ToastService.SendToast(result.alertDesc);
            }
        }
    }
}
