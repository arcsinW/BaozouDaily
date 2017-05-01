using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using BaoZouRiBao.Views;
using Windows.UI.Xaml.Controls;

namespace BaoZouRiBao.ViewModel
{
    public class MyFavoritePageViewModel : ViewModelBase
    {
        public MyFavoritePageViewModel()
        {
            Favorites = new IncrementalLoadingList<Document>(GetFavoritesDocument, () => { IsActive = false; }, () => { IsActive = true; });
            if (IsDesignMode)
            {
                LoadDesignData();
            }
        }

        #region Properties
        public IncrementalLoadingList<Document> Favorites { get; set; }

        private bool isActive = false;

        /// <summary>
        /// 是否正在加载
        /// </summary>
        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
                OnPropertyChanged();
            }
        }

        private bool isEmpty = false;

        /// <summary>
        /// 是否无数据
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return isEmpty;
            }

            set
            {
                isEmpty = value;
                OnPropertyChanged();
            }
        } 
        #endregion

        public void LoadDesignData()
        {
            string json = "[{\"author_avatar\":\"http://ww3.sinaimg.cn/large/005O2O52jw1f4slh5cvn6j305k05kaa4.jpg\",\"author_id\":null,\"author_name\":\"努力的doge\",\"author_summary\":null,\"body\":null,\"channels\":null,\"comment_count\":\"79\",\"commented\":\"false\",\"display_date\":null,\"display_type\":\"1\",\"document_id\":\"27919\",\"favorited\":\"true\",\"ga_prefix\":null,\"guide\":null,\"guide_image\":null,\"head\":null,\"hit_count\":\"40962\",\"hit_count_string\":\"4.1万\",\"publish_time\":null,\"published_at\":null,\"recommenders\":null,\"image\":null,\"section_name\":\"暴走天天看\",\"section_image\":\"http://ww4.sinaimg.cn/bmiddle/da4a9471gw1emu9mir52ij203c03c3yj.jpg\",\"key_words\":null,\"section_color\":\"FF9999\",\"section_id\":\"6\",\"share_url\":\"http://baozouribao.com/documents/27919\",\"original_tag\":null,\"tag_text\":null,\"thumbnail\":\"http://ww3.sinaimg.cn/large/005PPXEMjw1f4slh1e3s7j305k05kaaa.jpg\",\"timestamp\":null,\"title\":\"今天，doge要讲述一个悲伤的故事。。。\",\"url\":\"http://dailyapi.ibaozou.com/api/v31/documents/27919\",\"video_file_url\":null,\"video_image_url\":null,\"vote_count\":\"239\",\"voted\":\"false\"},{\"author_avatar\":\"http://ww4.sinaimg.cn/large/005O38k7jw1f0navob9dnj305k05k0sv.jpg\",\"author_id\":\"683368\",\"author_name\":\"吴彦祖的女人\",\"author_summary\":null,\"body\":null,\"channels\":null,\"comment_count\":\"80\",\"commented\":\"false\",\"display_date\":null,\"display_type\":\"2\",\"document_id\":\"27696\",\"favorited\":\"true\",\"ga_prefix\":null,\"guide\":null,\"guide_image\":null,\"head\":null,\"hit_count\":\"35105\",\"hit_count_string\":\"3.51万\",\"publish_time\":\"1465470000000\",\"published_at\":\"2016-06-09 19:00\",\"recommenders\":[{\"avatar\":\"http://ww4.sinaimg.cn/large/005O38k7jw1f0navob9dnj305k05k0sv.jpg\",\"id\":\"683368\",\"name\":\"吴彦祖的女人\"}],\"image\":null,\"section_name\":null,\"section_image\":null,\"key_words\":null,\"section_color\":null,\"section_id\":null,\"share_url\":null,\"original_tag\":null,\"tag_text\":null,\"thumbnail\":\"http://baozouribao-qiniu.b0.upaiyun.com/ribao_recommendation_image/2016/06/1465449544610-30e93963i6zx6xayr40sa1-ea6f7814dc820862c07c6b0a17628b3c\",\"timestamp\":\"1465449543\",\"title\":\"这位红三代牛X事迹一箩筐，还把三亿豪宅男主人和小公举藏了五年？！\",\"url\":\"http://mp.weixin.qq.com/s?__biz=MzA4NTI0Mjg2Nw==&mid=2652840480&idx=1&sn=bb0fe60ca701893251d3ca2bb628d67e&scene=0#wechat_redirect\",\"video_file_url\":null,\"video_image_url\":null,\"vote_count\":\"122\",\"voted\":\"false\"},{\"author_avatar\":\"http://ww4.sinaimg.cn/large/e8fffd59jw1ewj4gsa5mwj20dw0dwdgy.jpg\",\"author_id\":null,\"author_name\":\"大脸侠\",\"author_summary\":null,\"body\":null,\"channels\":null,\"comment_count\":\"140\",\"commented\":\"false\",\"display_date\":null,\"display_type\":\"1\",\"document_id\":\"27568\",\"favorited\":\"true\",\"ga_prefix\":null,\"guide\":null,\"guide_image\":null,\"head\":null,\"hit_count\":\"69462\",\"hit_count_string\":\"6.95万\",\"publish_time\":null,\"published_at\":null,\"recommenders\":null,\"image\":null,\"section_name\":\"电影收割机\",\"section_image\":\"http://ww1.sinaimg.cn/bmiddle/731ce372jw1evnuwui1kgj203c03cjra.jpg\",\"key_words\":null,\"section_color\":\"FF9999\",\"section_id\":\"124\",\"share_url\":\"http://baozouribao.com/documents/27568\",\"original_tag\":\"http://baozouribao-qiniu.b0.upaiyun.com/ribaopic/2016/05/1462853591015-b0edhpbqpf4lp02n4qk1b-21ee5b7a6e5b4c9bb2c2335a0bbb62dd\",\"tag_text\":null,\"thumbnail\":\"http://ww1.sinaimg.cn/large/005Oop2Wjw1f4p4m4fybtj303c03cwee.jpg\",\"timestamp\":null,\"title\":\"电影里那些让人毛骨悚然的表情，配合剧情食用更佳\",\"url\":\"http://dailyapi.ibaozou.com/api/v31/documents/27568\",\"video_file_url\":null,\"video_image_url\":null,\"vote_count\":\"324\",\"voted\":\"false\"},{\"author_avatar\":\"http://ww1.sinaimg.cn/large/0060idsIjw1ewj4ffcrynj30dw0dw405.jpg\",\"author_id\":null,\"author_name\":\"零小蝎\",\"author_summary\":null,\"body\":null,\"channels\":null,\"comment_count\":\"316\",\"commented\":\"false\",\"display_date\":null,\"display_type\":\"1\",\"document_id\":\"27654\",\"favorited\":\"true\",\"ga_prefix\":null,\"guide\":null,\"guide_image\":null,\"head\":null,\"hit_count\":\"68454\",\"hit_count_string\":\"6.85万\",\"publish_time\":null,\"published_at\":null,\"recommenders\":null,\"image\":null,\"section_name\":\"贵圈真乱\",\"section_image\":\"http://ww1.sinaimg.cn/thumbnail/7c1bc3cfjw1evobqxr369j20dw0dw0u7.jpg\",\"key_words\":null,\"section_color\":\"7CD095\",\"section_id\":\"125\",\"share_url\":\"http://baozouribao.com/documents/27654\",\"original_tag\":\"http://baozouribao-qiniu.b0.upaiyun.com/ribaopic/2016/05/1462853591015-b0edhpbqpf4lp02n4qk1b-21ee5b7a6e5b4c9bb2c2335a0bbb62dd\",\"tag_text\":null,\"thumbnail\":\"http://ww1.sinaimg.cn/large/005Oq1Rojw1f4o2i1gzo2j303c03cjrs.jpg\",\"timestamp\":null,\"title\":\"蔡依林打败三连冠的郭雪芙荣登全球百大美女第一名！\",\"url\":\"http://dailyapi.ibaozou.com/api/v31/documents/27654\",\"video_file_url\":null,\"video_image_url\":null,\"vote_count\":\"454\",\"voted\":\"false\"}]";
            var list = JsonHelper.Deserlialize<List<Document>>(json);
            foreach (var item in list)
            {
                Favorites.Add(item);
            }

            IsEmpty = false;
        }
 
        private async Task<IEnumerable<Document>> GetFavoritesDocument(uint count, string timeStamp)
        {
            List<Document> documents = new List<Document>();

            if (timeStamp.Equals("0"))
            {
                Favorites.NoMore();

                return documents;
            }

            var result = await ApiService.Instance.GetMyFavoriteAsync(timeStamp);
            if (result != null && result.Documents != null)
            {
                Favorites.TimeStamp = result.TimeStamp;

                foreach (var item in result.Documents)
                {
                    documents.Add(item);
                }
            }

            if (documents.Count == 0 && Favorites.Count == 0)
            {
                IsEmpty = true;
            }

            return documents;
        }

        public override async void Refresh()
        {
            IsActive = true;
            await Favorites.ClearAndReload();
            IsActive = false;
        }

        public void FavoriteListView_ItemClick(object sender, ItemClickEventArgs e)
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
    }
}
