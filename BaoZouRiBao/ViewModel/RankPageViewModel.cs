using BaoZouRiBao.Common;
using BaoZouRiBao.Enums;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.IncrementalCollection.DataSource;
using BaoZouRiBao.Model;
using BaoZouRiBao.ViewModel;
using BaoZouRiBao.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BaoZouRiBao.ViewModel
{
    public class RankPageViewModel : ViewModelBase
    {

        #region Properties
        public ObservableCollection<Document> ReadCollection { get; set; } = new ObservableCollection<Document>();

        public ObservableCollection<Document> VoteCollection { get; set; } = new ObservableCollection<Document>();

        public ObservableCollection<Document> CommentCollection { get; set; } = new ObservableCollection<Document>();

        /// <summary>
        /// 当前选择的排序日期index
        /// 0 day 1 week 2 month
        /// </summary>
        public int RankTimeIndex { get; set; } = 1;

        #endregion

        public bool isRankTimeChanged = false;

        #region Commands

        public RelayCommand RefreshReadCollectionCommand { get; set; }

        public RelayCommand RefreshVoteCollectionCommand { get; set; }

        public RelayCommand RefreshCommentCollectionCommand { get; set; }

        #endregion

        public RankPageViewModel()
        {
            RefreshReadCollectionCommand = new RelayCommand(async() =>
            {
                await RefreshReadCollectionAsync(RankTimeIndex);
            });

            RefreshVoteCollectionCommand = new RelayCommand(async() =>
            {
                await RefreshVoteCollectionAsync(RankTimeIndex);
            });

            RefreshCommentCollectionCommand = new RelayCommand(async() =>
            {
                await RefreshCommentCollectionAsync(RankTimeIndex);
            });

            if (IsDesignMode)
            {
                LoadDesignData();
            }
        }

        private async void LoadDesignData()
        {
            await RefreshReadCollectionAsync(RankTimeIndex);
            await RefreshVoteCollectionAsync(RankTimeIndex);
            await RefreshCommentCollectionAsync(RankTimeIndex);
        }
        
        /// <summary>
        /// 刷新阅读排行榜
        /// </summary>
        /// <param name="rankTimeIndex"></param>
        public async Task RefreshReadCollectionAsync(int rankTimeIndex)
        {
            ReadCollection.Clear();
            var result = await LoadRankDataAsync(RankTypeEnum.read, (RankTimeEnum)rankTimeIndex);
            if (result == null) return;
            foreach (var item in result)
            {
                ReadCollection.Add(item);
            }
        }

        /// <summary>
        /// 刷新 赞排行榜
        /// </summary>
        /// <param name="rankTimeIndex"></param>
        public async Task RefreshVoteCollectionAsync(int rankTimeIndex)
        {
            VoteCollection.Clear();
            var result = await LoadRankDataAsync(RankTypeEnum.vote, (RankTimeEnum)rankTimeIndex);
            if (result == null) return;
            foreach (var item in result)
            {
                VoteCollection.Add(item);
            }
        }

        /// <summary>
        /// 刷新评论排行榜
        /// </summary>
        /// <param name="rankTimeIndex"></param>
        public async Task RefreshCommentCollectionAsync(int rankTimeIndex)
        {
            CommentCollection.Clear();
            var result = await LoadRankDataAsync(RankTypeEnum.comment, (RankTimeEnum)rankTimeIndex);
            if (result == null) return;
            foreach (var item in result)
            {
                CommentCollection.Add(item);
            }
        }

        private async Task<List<Document>> LoadRankDataAsync(RankTypeEnum type, RankTimeEnum time)
        {
            var result = await ApiService.Instance.GetRank(type, time);
            return result?.Data;
        }

        public void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var story = e.ClickedItem as Document;
            if (story != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = "", WebViewUri = story.Url, DocumentId = story.DocumentId, DisplayType = story.DisplayType };
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
            }
        }
        
          
    }
}