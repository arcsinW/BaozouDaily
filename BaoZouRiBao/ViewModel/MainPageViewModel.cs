using BaoZouRiBao.Helper;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaoZouRiBao.IncrementalCollection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using BaoZouRiBao.Views;
using BaoZouRiBao.Common;
using BaoZouRiBao.Http;
using BaoZouRiBao.Controls;

namespace BaoZouRiBao.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            Documents = new IncrementalLoadingList<Document>(LoadDocuments, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { IsActive = false; ToastService.SendToast(e.Message); });
            Videos = new IncrementalLoadingList<Video>(LoadVideos, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { IsActive = false; ToastService.SendToast(e.Message); });
            Contributes = new IncrementalLoadingList<Contribute>(LoadContributes, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { IsActive = false; ToastService.SendToast(e.Message); });

            VoteVideoCommand = new RelayCommand(async (x) => await VoteVideoAsync((string)x));
            CommentCommand = new RelayCommand(x => NavigateToComment((string)x));

            User = DataShareManager.Current.User;
            DataShareManager.Current.DataChanged += Current_DataChanged;
        }

        ~MainPageViewModel()
        {
            DataShareManager.Current.DataChanged -= Current_DataChanged;
        }

        #region Load data functions

        private StringBuilder documentStringBuilder = new StringBuilder("0");

        /// <summary>
        /// 按时间戳加载首页数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Document>> LoadDocuments(uint count, string timeStamp)
        {
            string fileName = "latest_document.json";
            if (!ConnectionHelper.IsInternetAvailable)
            {

            }

            if (timeStamp.Equals(documentStringBuilder.ToString()))
            {
                Documents.NoMore();
                return null;
            }

            var stories = await ApiService.Instance.GetLatestDocumentAsync(timeStamp);

            if (stories != null && stories.Data != null)
            {
                List<Document> documents = new List<Document>();

                documentStringBuilder.Clear();
                documentStringBuilder.Append(timeStamp);
                Documents.TimeStamp = stories.TimeStamp;

                stories.Data?.ForEach(x => documents.Add(x));
                if (stories.TopStories != null)
                {
                    TopDocuments.Clear();
                    stories.TopStories?.ForEach(x => TopDocuments.Add(x));
                }

                return documents;
            }
            else
            {
                Documents.NoMore();
                if (Documents.Count == 0)
                {
                    IsDocumentsEmpty = true;
                }
            }

            return null;
        }


        private StringBuilder videoStringBuilder = new StringBuilder("0");

        private async Task<IEnumerable<Video>> LoadVideos(uint count, string timeStamp)
        {
            if (timeStamp.Equals(videoStringBuilder.ToString()))
            {
                Videos.NoMore();
                return null;
            }

            var result = await ApiService.Instance.GetLatestVideoAsync(timeStamp);

            if (result != null)
            {
                List<Video> videos = new List<Video>();

                videoStringBuilder.Clear();
                videoStringBuilder.Append(timeStamp);
                Videos.TimeStamp = result.TimeStamp;
                
                result.Videos?.ForEach(x => videos.Add(x));
                return videos;
            }
            else
            {
                Videos.NoMore();
                if (Videos.Count == 0)
                {
                    IsVideosEmpty = true;
                }
            }

            return null;
        }


        private StringBuilder contributeStringBuilder = new StringBuilder("0");

        private async Task<IEnumerable<Contribute>> LoadContributes(uint count, string timeStamp)
        {
            if (timeStamp.Equals(contributeStringBuilder.ToString()))
            {
                Videos.NoMore();
                return null;
            }

            var result = await ApiService.Instance.GetLatestContributeAsync(timeStamp.ToString());

            if (result != null)
            {
                contributeStringBuilder.Clear();
                contributeStringBuilder.Append(timeStamp);
                Contributes.TimeStamp = result.TimeStamp;

                List<Contribute> contributes = new List<Contribute>();

                result.Contributes?.ForEach(x => contributes.Add(x));
                return contributes;
            }
            else
            {
                Contributes.NoMore();
                if (Contributes.Count == 0)
                {
                    IsContributesEmpty = true;
                }
            }
             
            return null;
        }
        #endregion 

        #region Properties

        /// <summary>
        /// 首页数据
        /// </summary>
        public IncrementalLoadingList<Document> Documents { get; set; }

        /// <summary>
        /// 首页轮播数据
        /// </summary>
        public ObservableCollection<Document> TopDocuments { get; set; } = new ObservableCollection<Document>();
        
        public IncrementalLoadingList<Video> Videos { get; set; }

        public IncrementalLoadingList<Contribute> Contributes { get; set; }
         

        public bool IsDocumentsEmpty { get; set; }

        public bool IsContributesEmpty { get; set; }

        public bool IsVideosEmpty { get; set; }


        private User user;
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private bool isActive;
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

        public Document SelectedItem { get; set; }
        #endregion

        /// <summary>
        /// 点赞Command
        /// </summary>
        public RelayCommand VoteVideoCommand { get; set; }

        /// <summary>
        /// 跳转到对应的评论页面
        /// </summary>
        public RelayCommand CommentCommand { get; set; }

        private void Current_DataChanged()
        {
            this.User = DataShareManager.Current.User;
        }

        /// <summary>
        /// 点赞 视频
        /// </summary>
        /// <param name="videoId"></param>
        public async Task VoteVideoAsync(string videoId)
        {
            var result = await ApiService.Instance.VoteAsync(videoId);
            if (result != null)
            {
                ToastService.SendToast(result.Description);
            }
        }

        public void NavigateToComment(string videoId)
        {
            if (!string.IsNullOrEmpty(videoId))
            {
                NavigationHelper.DetailFrameNavigate(typeof(CommentPage), videoId);
            }
        }

        #region Refresh's methods
        /// <summary>
        /// 刷新首页
        /// </summary>
        /// <returns></returns>
        public async Task RefreshDocument()
        {
            IsActive = true;
            documentStringBuilder.Clear();
            documentStringBuilder.Append("0");
            await Documents.ClearAndReloadAsync();
            IsActive = false;
        }

        /// <summary>
        /// 刷新投稿
        /// </summary>
        /// <returns></returns>
        public async Task RefreshContribute()
        {
            IsActive = true;
            contributeStringBuilder.Clear();
            contributeStringBuilder.Append("0");
            await Contributes.ClearAndReloadAsync();
            IsActive = false;
        }

        /// <summary>
        /// 刷新视频
        /// </summary>
        /// <returns></returns>
        public async Task RefreshVideo()
        {
            IsActive = true;
            videoStringBuilder.Clear();
            videoStringBuilder.Append("0");
            await Videos.ClearAndReloadAsync();
            IsActive = false;
        }
        #endregion

        #region ItemClick's methods
        public void documentListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var story = e.ClickedItem as Document;
            if (story != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = story.Title, WebViewUri = story.Url, DocumentId = story.DocumentId, DisplayType = story.DisplayType };
                NavigationHelper.DetailFrameNavigate(typeof(WebViewPage), parameter);
            }
        }

        public void videoListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var video = e.ClickedItem as Video;
            if (video != null)
            {
                NavigationHelper.DetailFrameNavigate(typeof(VideoPage), video.DocumentId);
            }
        }

        public void contributeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var contribute = e.ClickedItem as Contribute;
            if (contribute != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = contribute.Title, DocumentId = contribute.DocumentId, DisplayType = contribute.DisplayType, WebViewUri = contribute.Url };
                NavigationHelper.DetailFrameNavigate(typeof(WebViewPage), parameter);
            }
        } 
        #endregion
    }
}
