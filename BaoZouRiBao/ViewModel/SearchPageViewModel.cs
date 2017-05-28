using BaoZouRiBao.Controls;
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
        public SearchPageViewModel()
        {
            SearchResults = new IncrementalLoadingList<Document>(SearchAsync, () => { IsActive = false; }, () => { IsActive = true; }, (Exception e) => { IsActive = false; ToastService.SendToast(e.Message); });

            if (DesignMode.DesignModeEnabled)
            {
                Keyword = "标准";
                Search();
            }
        }

        #region Properties

        public IncrementalLoadingList<Document> SearchResults { get; set; } 

        private string keyword;

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
        
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="count"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Document>> SearchAsync(uint count, int pageIndex)
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
            await SearchResults.ClearAndReloadAsync();
        } 
    }
}
