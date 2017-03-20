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

        private bool isEmpty;

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
        }

        public async Task<IEnumerable<Document>> GetReadHistory(uint count, string timeStamp)
        {
            List<Document> histories = new List<Document>();
            
            if (timeStamp.Equals("0"))
            {
                ReadHistories.NoMore();

                return histories;
            }
            var result = await ApiService.Instance.GetMyReadHistory(timeStamp);
            if (result != null && result.Documents != null)
            {
                ReadHistories.TimeStamp = result.TimeStamp;

                foreach (var item in result.Documents)
                {
                    histories.Add(item);
                }
            }
            IsEmpty = (histories.Count == 0);
            return histories;
        }

        public async void RefreshReadHistories()
        {
            await ReadHistories.ClearAndReload();
        }

        public void documentListView_ItemClick(object sender, ItemClickEventArgs e)
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
