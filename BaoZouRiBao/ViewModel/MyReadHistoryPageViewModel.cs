using BaoZouRiBao.Controls;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using BaoZouRiBao.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BaoZouRiBao.ViewModel
{
    public class MyReadHistoryPageViewModel : ViewModelBase
    {
        #region Properties
        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        }

        private bool isEmpty = false;

        public bool IsEmpty
        {
            get { return isEmpty; }
            set { isEmpty = value; OnPropertyChanged(); }
        }

        #endregion

        public IncrementalLoadingList<Document> ReadHistories { get; set; }

        public MyReadHistoryPageViewModel()
        {
            ReadHistories = new IncrementalLoadingList<Document>(GetReadHistory, () => { IsActive = false; }, () => { IsActive = true; });

            if(IsDesignMode)
            {
                LoadDesignTimeData();
            }
        }


        public void LoadDesignTimeData()
        {
            string json = "[{\"author_avatar\":\"http://ww3.sinaimg.cn/thumb300/0061W1jvgw1f6ta2codxoj305k05k0su.jpg\",\"author_id\":null,\"author_name\":\"争气战斗姬\",\"author_summary\":null,\"body\":null,\"channels\":null,\"comment_count\":\"36\",\"commented\":\"false\",\"display_date\":null,\"display_type\":\"1\",\"document_id\":\"43664\",\"favorited\":\"false\",\"ga_prefix\":null,\"guide\":null,\"guide_image\":null,\"head\":null,\"hit_count\":\"8337\",\"hit_count_string\":\"8千\",\"publish_time\":null,\"published_at\":null,\"recommenders\":null,\"image\":null,\"section_name\":\"全网最好笑\",\"section_image\":\"http://ww3.sinaimg.cn/mw690/da4a9471tw1eqz3q6av98j203c03ct8q.jpg\",\"key_words\":null,\"section_color\":\"80CFCA\",\"section_id\":\"9\",\"share_url\":\"http://baozouribao.com/documents/43664\",\"original_tag\":\"http://baozouribao-qiniu.b0.upaiyun.com/ribaopic/2016/05/1462853591015-b0edhpbqpf4lp02n4qk1b-21ee5b7a6e5b4c9bb2c2335a0bbb62dd\",\"tag_text\":null,\"thumbnail\":\"http://ww3.sinaimg.cn/large/0066cLprjw1fd9t35dv4aj303c03cmx8.jpg\",\"timestamp\":null,\"title\":\"现在的产品真的是越来越走肾了！\",\"url\":\"http://dailyapi.ibaozou.com/api/v31/documents/43664\",\"video_file_url\":null,\"video_image_url\":null,\"vote_count\":\"284\",\"voted\":\"false\"},{\"author_avatar\":\"http://ww3.sinaimg.cn/thumb300/0061W1jvgw1f6ta2codxoj305k05k0su.jpg\",\"author_id\":null,\"author_name\":\"争气战斗姬\",\"author_summary\":null,\"body\":null,\"channels\":null,\"comment_count\":\"133\",\"commented\":\"false\",\"display_date\":null,\"display_type\":\"1\",\"document_id\":\"42644\",\"favorited\":\"false\",\"ga_prefix\":null,\"guide\":null,\"guide_image\":null,\"head\":null,\"hit_count\":\"8163\",\"hit_count_string\":\"8千\",\"publish_time\":null,\"published_at\":null,\"recommenders\":null,\"image\":null,\"section_name\":\"全网最好笑\",\"section_image\":\"http://ww3.sinaimg.cn/mw690/da4a9471tw1eqz3q6av98j203c03ct8q.jpg\",\"key_words\":null,\"section_color\":\"80CFCA\",\"section_id\":\"9\",\"share_url\":\"http://baozouribao.com/documents/42644\",\"original_tag\":\"http://baozouribao-qiniu.b0.upaiyun.com/ribaopic/2016/05/1462853591015-b0edhpbqpf4lp02n4qk1b-21ee5b7a6e5b4c9bb2c2335a0bbb62dd\",\"tag_text\":null,\"thumbnail\":\"http://ww1.sinaimg.cn/large/006sB4ohjw1fclfm4wxchj303c03c74a.jpg\",\"timestamp\":null,\"title\":\"单身狗怎么过情人节？\",\"url\":\"http://dailyapi.ibaozou.com/api/v31/documents/42644\",\"video_file_url\":null,\"video_image_url\":null,\"vote_count\":\"392\",\"voted\":\"false\"},{\"author_avatar\":\"http://ww3.sinaimg.cn/large/005O2O52jw1f4slh5cvn6j305k05kaa4.jpg\",\"author_id\":null,\"author_name\":\"努力的doge\",\"author_summary\":null,\"body\":null,\"channels\":null,\"comment_count\":\"79\",\"commented\":\"false\",\"display_date\":null,\"display_type\":\"1\",\"document_id\":\"27919\",\"favorited\":\"true\",\"ga_prefix\":null,\"guide\":null,\"guide_image\":null,\"head\":null,\"hit_count\":\"40962\",\"hit_count_string\":\"4.1万\",\"publish_time\":null,\"published_at\":null,\"recommenders\":null,\"image\":null,\"section_name\":\"暴走天天看\",\"section_image\":\"http://ww4.sinaimg.cn/bmiddle/da4a9471gw1emu9mir52ij203c03c3yj.jpg\",\"key_words\":null,\"section_color\":\"FF9999\",\"section_id\":\"6\",\"share_url\":\"http://baozouribao.com/documents/27919\",\"original_tag\":null,\"tag_text\":null,\"thumbnail\":\"http://ww3.sinaimg.cn/large/005PPXEMjw1f4slh1e3s7j305k05kaaa.jpg\",\"timestamp\":null,\"title\":\"今天，doge要讲述一个悲伤的故事。。。\",\"url\":\"http://dailyapi.ibaozou.com/api/v31/documents/27919\",\"video_file_url\":null,\"video_image_url\":null,\"vote_count\":\"239\",\"voted\":\"false\"},{\"author_avatar\":\"http://wx.qlogo.cn/mmopen/3Lqm1xHojtbDs76UDEA4Sw0VWXMysl45lKdBcMVvLtJDKQhVsFCBGnf8qk17iaYWkRCe8ctV5XdeicMqLmJjhpCzscyhQYjzBj/132\",\"author_id\":\"683379\",\"author_name\":\"漫步云端\",\"author_summary\":null,\"body\":null,\"channels\":null,\"comment_count\":\"59\",\"commented\":\"false\",\"display_date\":null,\"display_type\":\"2\",\"document_id\":\"44276\",\"favorited\":\"false\",\"ga_prefix\":null,\"guide\":null,\"guide_image\":null,\"head\":null,\"hit_count\":\"13047\",\"hit_count_string\":\"1.3万\",\"publish_time\":\"1489910400000\",\"published_at\":\"2017-03-19 16:00\",\"recommenders\":[{\"avatar\":\"http://wx.qlogo.cn/mmopen/3Lqm1xHojtbDs76UDEA4Sw0VWXMysl45lKdBcMVvLtJDKQhVsFCBGnf8qk17iaYWkRCe8ctV5XdeicMqLmJjhpCzscyhQYjzBj/132\",\"id\":\"683379\",\"name\":\"漫步云端\"}],\"image\":null,\"section_name\":null,\"section_image\":null,\"key_words\":null,\"section_color\":null,\"section_id\":null,\"share_url\":null,\"original_tag\":null,\"tag_text\":null,\"thumbnail\":\"http://wx4.sinaimg.cn/large/005O1SH7ly1fds7i706mmj306e06e3yh.jpg\",\"timestamp\":\"1489910424\",\"title\":\"女仆女仆女仆！御姐御姐御姐！\",\"url\":\"http://mp.weixin.qq.com/s?__biz=MjM5ODM3MTUwMA==&mid=2650777799&idx=1&sn=628b1b487f45416b549182274a08de79&chksm=bec0c10489b74812e5f48d38db57f870ab6395deb74155850d2d4d971883196a6048a068ad76&scene=0#wechat_redirect\",\"video_file_url\":null,\"video_image_url\":null,\"vote_count\":\"78\",\"voted\":\"false\"},{\"author_avatar\":\"http://ww1.sinaimg.cn/large/005ZRYM4jw1exxzyp41ctj305k05kgle.jpg\",\"author_id\":\"683410\",\"author_name\":\"Miss.晴天\",\"author_summary\":null,\"body\":null,\"channels\":null,\"comment_count\":\"67\",\"commented\":\"false\",\"display_date\":null,\"display_type\":\"2\",\"document_id\":\"44270\",\"favorited\":\"false\",\"ga_prefix\":null,\"guide\":null,\"guide_image\":null,\"head\":null,\"hit_count\":\"18407\",\"hit_count_string\":\"1.84万\",\"publish_time\":\"1489903200000\",\"published_at\":\"2017-03-19 14:00\",\"recommenders\":[{\"avatar\":\"http://ww1.sinaimg.cn/large/005ZRYM4jw1exxzyp41ctj305k05kgle.jpg\",\"id\":\"683410\",\"name\":\"Miss.晴天\"}],\"image\":null,\"section_name\":null,\"section_image\":null,\"key_words\":null,\"section_color\":null,\"section_id\":null,\"share_url\":null,\"original_tag\":null,\"tag_text\":null,\"thumbnail\":\"http://wx2.sinaimg.cn/large/006sGguFly1fds41gom4uj30hl0hmwex.jpg\",\"timestamp\":\"1489903232\",\"title\":\"为了摆脱“赫敏”标签，艾玛·沃森与大表哥上演唯美人兽恋？\",\"url\":\"http://mp.weixin.qq.com/s?__biz=MjM5MjcwMzI1Ng==&mid=2650365189&idx=1&sn=4ceba4447363c33d2ae4d613f8931575&chksm=beafcb7289d842643ac367e5d48361073c87e780d4edee1fde4cc1fffbea6664363f8e667936&scene=0#wechat_redirect\",\"video_file_url\":null,\"video_image_url\":null,\"vote_count\":\"68\",\"voted\":\"false\"}]";
            var histories = JsonHelper.Deserlialize<List<Document>>(json);
            foreach (var item in histories)
            {
                ReadHistories.Add(item);
            }
        }

        public async Task<IEnumerable<Document>> GetReadHistory(uint count, string timeStamp)
        {
            List<Document> histories = new List<Document>();
            
            if (timeStamp.Equals("0"))
            {
                ReadHistories.NoMore();

                return histories;
            }
            var result = await ApiService.Instance.GetMyReadHistoryAsync(timeStamp);
            if (result != null && result.Documents != null)
            {
                ReadHistories.TimeStamp = result.TimeStamp;

                foreach (var item in result.Documents)
                {
                    histories.Add(item);
                }
            }

            if (histories.Count == 0 && ReadHistories.Count == 0)
            {
                IsEmpty = true;
            }

            return histories;
        }

        public async void RefreshReadHistories()
        {
            await ReadHistories.ClearAndReloadAsync();
        }

        public void documentListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var document = e.ClickedItem as Document;
            if (document != null)
            { 
                if (!document.DisplayType.Equals("3"))
                {
                    WebViewParameter parameter = new WebViewParameter() { Title = "", WebViewUri = document.Url, DocumentId = document.DocumentId, DisplayType = document.DisplayType };
 
                    MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
                }
                else
                {
                    NavigationHelper.DetailFrameNavigate(typeof(VideoPage), document.DocumentId);
                }
            }
        }

        public async void ClearHistories()
        {
            var result = await ApiService.Instance.ClearReadHistoryAsync();
            if (result != null && result.Status.Equals("1000"))
            {
                ToastService.SendToast(result.alertDesc);
                ReadHistories.Clear();
            }
        }
    }
}
