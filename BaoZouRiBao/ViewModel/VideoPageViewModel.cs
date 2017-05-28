using BaoZouRiBao.Controls;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoZouRiBao.ViewModel
{
    public class VideoPageViewModel :  ViewModelBase
    {
        public VideoPageViewModel()
        {
            HottestComments = new ObservableCollection<Comment>();
            LatestComments = new ObservableCollection<Comment>();
            if (IsDesignMode)
            {
                LoadDesignData();
            } 
        }

        #region Properties
        public ObservableCollection<Comment> HottestComments { get; set; }

        public ObservableCollection<Comment> LatestComments { get; set; }

        private Video video;
        public Video Video
        {
            get
            {
                return video;
            }
            set
            {
                video = value;
                OnPropertyChanged();
            }
        }

        private DocumentExtra documentExtra;
        public DocumentExtra DocumentExtra
        {
            get
            {
                return documentExtra;
            }
            set
            {
                documentExtra = value;
                OnPropertyChanged();
            }
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        }

        private bool isFavorite;

        public bool IsFavorite
        {
            get { return isFavorite; }
            set { isFavorite = value; OnPropertyChanged(); }
        }


        #endregion

        /// <summary>
        /// 加载设计时数据
        /// </summary>
        private void LoadDesignData()
        {
            string commentJson = @"{'hottest':[],'latest':[{'id':2558811,'content':'大家DJ','readable_time':'2016 - 06 - 14T21: 54:27 + 08:00','likes':0,'dislikes':0,'time':1465912467000,'score':0,'own':false,'like':false,'dislike':false,'user':{'id':716830,'name':'念亲恩','real_avatar_url':'http://q.qlogo.cn/qqapp/1101108234/EE3077AFDAA4191E5F2E91E04EB28A4B/100'},'hottest':false},{'id':2558810,'content':'足球怎么来的你造吗','readable_time':'2016-06-14T21:54:13+08:00','likes':0,'dislikes':0,'time':1465912453000,'score':0,'own':false,'like':false,'dislike':false,'user':{'id':716830,'name':'念亲恩','real_avatar_url':'http://q.qlogo.cn/qqapp/1101108234/EE3077AFDAA4191E5F2E91E04EB28A4B/100'},'parent':{'id':2558307,'user_id':420257,'user_name':'李尼玛b先生'},'hottest':false},{'id':2558792,'content':'这不是我徒弟吗','readable_time':'2016-06-14T21:48:53+08:00','likes':0,'dislikes':0,'time':1465912133000,'score':0,'own':false,'like':false,'dislike':false,'user':{'id':510673,'name':'大圣','real_avatar_url':'http://zhihu.b0.upaiyun.com/avatar/9a4b64be0'},'hottest':false},{'id':2558307,'content':'街头花式足球与正经足球还是有很大差距，看着很炫，其实放在足球场上没有一点卵用。就当观赏了。','readable_time':'2016-06-14T19:50:14+08:00','likes':0,'dislikes':0,'time':1465905014000,'score':0,'own':false,'like':false,'dislike':false,'user':{'id':420257,'name':'李尼玛b先生','real_avatar_url':'http://zhihu.b0.upaiyun.com/avatar/64b64c203'},'hottest':false},{'id':2558091,'content':'为啥他们不去踢世界杯呢','readable_time':'2016-06-14T18:48:25+08:00','likes':0,'dislikes':0,'time':1465901305000,'score':0,'own':false,'like':false,'dislike':false,'user':{'id':713548,'name':'一个孤独的人','real_avatar_url':'http://q.qlogo.cn/qqapp/1101108234/8CCC1A0A0433A42483D98C8C142B44CE/100'},'hottest':false},{'id':2558087,'content':'额','readable_time':'2016-06-14T18:46:15+08:00','likes':0,'dislikes':0,'time':1465901175000,'score':0,'own':false,'like':false,'dislike':false,'user':{'id':712209,'name':'戮影','real_avatar_url':'http://q.qlogo.cn/qqapp/1101108234/80FCD4B5360FF6DD048FE2898E282E01/100'},'hottest':false},{'id':2558030,'content':'尼玛我还以为开头卡成那样，都是套路','readable_time':'2016-06-14T18:35:35+08:00','likes':0,'dislikes':0,'time':1465900535000,'score':0,'own':false,'like':false,'dislike':false,'user':{'id':451213,'name':'面包德莫妮翁','real_avatar_url':'http://wanzao2.b0.upaiyun.com/baozouribao/256a7a70abcb0133439352540032331e.png'},'hottest':false},{'id':2558024,'content':'也许各国人天赋不同，但国足努力了','readable_time':'2016-06-14T18:35:02+08:00','likes':0,'dislikes':0,'time':1465900502000,'score':0,'own':false,'like':false,'dislike':false,'user':{'id':716830,'name':'念亲恩','real_avatar_url':'http://q.qlogo.cn/qqapp/1101108234/EE3077AFDAA4191E5F2E91E04EB28A4B/100'},'hottest':false},{'id':2557849,'content':'沙发','readable_time':'2016-06-14T18:14:16+08:00','likes':0,'dislikes':0,'time':1465899256000,'score':0,'own':false,'like':false,'dislike':false,'user':{'id':568455,'name':'这不科学！','real_avatar_url':'http://zhihu.b0.upaiyun.com/avatar/c6f8ff6c9'},'hottest':false}]}";
            var comments = JsonHelper.Deserlialize<DocumentComment>(commentJson);
            if (comments != null)
            {
                foreach (var item in comments.Hottest)
                {
                    HottestComments.Add(item);
                }

                foreach (var item in comments.Latest)
                {
                    LatestComments.Add(item);
                }
            }

            string documentExtraJson = @"{'document_id':27970,'display_type':3,'play_count':1835,'comment_count':9,'vote_count':16,'play_count_string':'1835','favorited':false,'voted':false,'commented':false,'channels':[]}";
            DocumentExtra = JsonHelper.Deserlialize<DocumentExtra>(documentExtraJson);

            string documentJson = @"{'document_id':27970,'display_type':3,'play_time':220,'summary':'','title':'比利时玩家的街头足球技巧秀，简直酷炫','image':'http://7o51ui.com2.z0.glb.qiniucdn.com/ribaovideo/2016/06/1465798849010-rgwstwr7sa4i55ufi2dlnn-abe140d52b438b18fb235e91f9234357','play_count':1835,'comment_count':9,'vote_count':16,'file_url':'http://gslb.miaopai.com/stream/tPoC3WMUetPHHKM9JXO~VA__.mp4','share_url':'http://baozouribao.com/documents/27970','publish_time':1465898400000,'play_count_string':'1835','published_at':'2016-06-14 18:00','favorited':false,'voted':false,'commented':false,'channels':[]}";
            Video = JsonHelper.Deserlialize<Video>(documentJson);
        }

        public async void LoadData(string documentId)
        {
            Video = await ApiService.Instance.GetVideoAsync(documentId);
            if (Video != null)
            {
                Video = video;
                IsFavorite = Video.Favorited;
            }

            DocumentExtra = await ApiService.Instance.GetDocumentExtraAsync(documentId);
            
            var comments = await ApiService.Instance.GetDocumentCommentAsync(documentId);

            if (comments != null)
            {
                foreach (var item in comments.Hottest)
                {
                    HottestComments.Add(item);
                }

                foreach (var item in comments.Latest)
                {
                    LatestComments.Add(item);
                }
            }
        }

        /// <summary>
        /// 收藏
        /// </summary>
        /// <returns></returns>
        public async void Favorite(object sender, RoutedEventArgs e)
        {
            if (DocumentExtra != null)
            {
                var res = await ApiService.Instance.FavoriteAsync(DocumentExtra.DocumentId);
                if (res != null && !string.IsNullOrEmpty(res.Status))
                {
                    if (res.Status.Equals("2006"))
                    {
                        IsFavorite = true;
                    }
                }
            }
        }

        /// <summary>
        /// 点赞视频
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task Vote()
        {
            if (Video != null)
            {
                var result = await ApiService.Instance.VoteAsync(Video.DocumentId);
                if (result != null)
                {
                    if (result.Status.Equals("1000")) //点赞成功
                    {
                        DocumentExtra.VoteCount = result.Data.Count;
                    }
                    ToastService.SendToast(result.AlertDesc);
                }
            }

            VoteTask();
        }

        /// <summary>
        /// 完成点赞文章的任务
        /// </summary>
        public void VoteTask()
        {
            BaoZouTaskManager.VoteDocument();
        }
    }
}
