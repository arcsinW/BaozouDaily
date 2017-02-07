using BaoZouRiBao.Core.Enums;
using BaoZouRiBao.Core.Helper;
using BaoZouRiBao.Core.IncrementalCollection;
using BaoZouRiBao.Core.IncrementalCollection.DataSource;
using BaoZouRiBao.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.ViewModel
{
    public class RankPageViewModel : ViewModelBase
    {
        public IncrementalLoadingCollection<RankDataSource, Document> ReadCollection { get; set; }

        public IncrementalLoadingCollection<RankDataSource, Document> VoteCollection { get; set; }

        public IncrementalLoadingCollection<RankDataSource, Document> CommentCollection { get; set; }

        public RelayCommand RefreshReadCollectionCommand { get; set; }

        public RelayCommand RefreshVoteCollectionCommand { get; set; }

        public RelayCommand RefreshCommentCollectionCommand { get; set; }
         
        private RankTimeEnum _time = RankTimeEnum.day;
         
        public void SetTime(RankTimeEnum time)
        {
            _time = time;
        }
             
        public RankPageViewModel()
        {
            LoadData();
            RefreshReadCollectionCommand = new RelayCommand(() =>
            {
                RefreshReadCollection();
            });
        }

        public RankPageViewModel(RankTimeEnum time)
        {
            _time = time;
            RefreshReadCollectionCommand = new RelayCommand(() =>
            {
                RefreshReadCollection();
            });
            LoadData();
        }

        public void LoadData()
        {
            ReadCollection = new IncrementalLoadingCollection<RankDataSource, Document>(new RankDataSource(RankTypeEnum.read, _time));

            VoteCollection = new IncrementalLoadingCollection<RankDataSource, Document>(new RankDataSource(RankTypeEnum.vote, _time));

            CommentCollection = new IncrementalLoadingCollection<RankDataSource, Document>(new RankDataSource(RankTypeEnum.comment, _time));
        }

        public void RefreshReadCollection()
        {
            ReadCollection.Clear();
            ReadCollection.Refresh();
        }

        public void RefreshVoteCollection()
        {

        }

        public void RefreshCommentCollection()
        {

        }

        public void ReloadReadCollection(int timeIndex)
        {
            ReadCollection.Clear();
            switch(timeIndex)
            {
                case 0: //今天 
                    ReadCollection.Refresh();
                    break; 
                case 1:  // 7天
                    break;
                case 2: // 30天
                    break;
            }   
        }

        public void ReloadVoteCollection()
        {

        }

        public void ReloadCommentCollection(int timeIndex)
        {

        }
    }
}
