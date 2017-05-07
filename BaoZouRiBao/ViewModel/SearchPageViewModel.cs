using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using BaoZouRiBao.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;

namespace BaoZouRiBao.ViewModel
{
    public class SearchPageViewModel : ViewModelBase
    {
        #region Properties
        public SearchResultCollection ResultCollection { get; set; } = new SearchResultCollection();

        public IncrementalLoadingList<Document> SearchResults { get; set; } 

        private string keyword = "暴走日报";

        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; OnPropertyChanged(); }
        }

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
            set { isEmpty = value; }
        }

        #endregion


        public SearchPageViewModel()
        {
            SearchResults = new IncrementalLoadingList<Document>(Search);

            ResultCollection.OnDataLoadingEvent += ResultCollection_OnDataLoadingEvent;
            ResultCollection.OnDataLoadedEvent += ResultCollection_OnDataLoadedEvent;

            if(DesignMode.DesignModeEnabled)
            {
                Keyword = "标准";
                Search();
            }
        }

        private async Task<IEnumerable<Document>> Search(uint count, int pageIndex)
        {
            List<Document> documents = new List<Document>();
            if (string.IsNullOrWhiteSpace(Keyword))
            {
                count = 0;
                return documents;
            }

            var searchResult = await ApiService.Instance.SearchAsync(Keyword, pageIndex++);
            if (searchResult == null || searchResult.Documents.Length == 0)
            {
                SearchResults.NoMore();
            }
            else
            {
                foreach (var item in searchResult.Documents)
                {
                    documents.Add(item);
                }
            }

            if(documents.Count == 0 && SearchResults.Count == 0)
            {
                IsEmpty = true;
            }

            return documents;
        }
          
        public void SearchListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var story = e.ClickedItem as Document;
            if (story != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = "", WebViewUri = story.Url, DocumentId = story.DocumentId, DisplayType = story.DisplayType };
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
            }
        }

        public async void Search()
        {
            //ResultCollection.SetKeyword(Keyword); 
            await SearchResults.ClearAndReloadAsync();
        }

        private void ResultCollection_OnDataLoadingEvent()
        {
            IsActive = true;
        }

        private void ResultCollection_OnDataLoadedEvent()
        {
            IsActive = false;
        }
    }
}
