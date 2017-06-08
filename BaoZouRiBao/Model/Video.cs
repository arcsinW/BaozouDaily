using BaoZouRiBao.Common;
using BaoZouRiBao.Controls;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    public class Video : ModelBase
    {
        #region Properties
        [JsonProperty(PropertyName = "channels")]
        public Channel[] Channels { get; set; }

        [JsonProperty(PropertyName = "comment_count")]
        public string CommentCount { get; set; }

        [JsonProperty(PropertyName = "display_type")]
        public string DisplayType { get; set; }

        [JsonProperty(PropertyName = "document_id")]
        public string DocumentId { get; set; }

        private bool favorited = false;
        /// <summary>
        /// 是否已收藏
        /// </summary>
        [JsonProperty(PropertyName = "favorited")]
        public bool Favorited
        {
            get
            {
                return favorited;
            }
            set
            {
                favorited = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(PropertyName = "file_url")]
        public string FileUrl { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "play_count")]
        public string PlayCount { get; set; }

        [JsonProperty(PropertyName = "play_count_string")]
        public string PlayCountString { get; set; }

        [JsonProperty(PropertyName = "play_time")]
        public string PlayTime { get; set; }

        [JsonProperty(PropertyName = "publish_time")]
        public string PublishTime { get; set; }

        [JsonProperty(PropertyName = "share_url")]
        public string ShareUrl { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        private int voteCount = 0;
        [JsonProperty(PropertyName = "vote_count")]
        public int VoteCount
        {
            get
            {
                return voteCount;
            }
            set
            {
                voteCount = value;
                OnPropertyChanged();
            }
        }

        private bool voted = false;
        /// <summary>
        /// 是否赞过
        /// </summary>
        [JsonProperty(PropertyName = "voted")]
        public bool Voted
        {
            get
            {
                return voted;
            }
            set
            {
                voted = value;
                OnPropertyChanged();
            }
        }
        #endregion
         
        #region Commands
        /// <summary>
        /// 评论 此评论
        /// </summary>
        /// <param name="content"></param>
        public async void Reply(string content)
        {
            await ApiService.Instance.CommentAsync(DocumentId, content);
        }

        public async void Share()
        {
            await WeChatHelper.ShareWebAsync(Title, "", ShareUrl, Image);
        }

        /// <summary>
        /// 点赞
        /// </summary>
        public RelayCommand VoteCommand { get; set; }

        /// <summary>
        /// 分享
        /// </summary>
        public RelayCommand ShareCommand { get; set; }

        public Video()
        {
            VoteCommand = new RelayCommand(() => { Vote(); });
            ShareCommand = new RelayCommand(() => { Share(); });
        }

        /// <summary>
        /// 点赞此评论
        /// </summary>
        public async void Vote()
        {
            var result = await ApiService.Instance.VoteAsync(DocumentId);
            if (result != null)
            {
                if (result.Status == "1000")
                {
                    Voted = true;
                    VoteCount++;
                }
                ToastService.SendToast(result.AlertDesc);
            }
        }
        #endregion
    }
}
