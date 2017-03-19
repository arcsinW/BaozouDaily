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
        public SearchResultCollection ResultCollection { get; set; } = new SearchResultCollection();

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


        public SearchPageViewModel()
        {
            ResultCollection.OnDataLoadingEvent += ResultCollection_OnDataLoadingEvent;
            ResultCollection.OnDataLoadedEvent += ResultCollection_OnDataLoadedEvent;

            if(DesignMode.DesignModeEnabled)
            {
                Keyword = "标准";
                Search();
            }
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


        public void Search()
        {
            //ResultCollection = new SearchResultCollection(Keyword);

            //OnPropertyChanged("ResultCollection");

            ResultCollection.SetKeyword(Keyword);
            //var searchResult = await ApiService.Instance.Search(Keyword, 1);
            //if(searchResult!=null && searchResult.Documents?.Length >0)
            //{
            //    ResultCollection.Clear();
            //    foreach (var item in searchResult.Documents)
            //    {
            //        ResultCollection.Add(item);
            //    }
            //}
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
