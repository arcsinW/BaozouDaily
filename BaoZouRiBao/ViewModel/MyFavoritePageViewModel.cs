using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using BaoZouRiBao.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BaoZouRiBao.ViewModel
{
    public class MyFavoritePageViewModel : ViewModelBase
    {
        public IncrementalLoadingList<Document> Favorites { get; set; }
     
        public MyFavoritePageViewModel()
        {
            Favorites = new IncrementalLoadingList<Document>(GetFavoritesDocument);
        }

        private async Task<IEnumerable<Document>> GetFavoritesDocument(uint count,string timeStamp)
        {
            List<Document> documents = new List<Document>();

            if(timeStamp.Equals("0"))
            {
                Favorites.NoMore();
                return documents;
            }
            var result = await ApiService.Instance.GetMyFavorite(timeStamp);
            if(result!=null && result.Documents !=null)
            {
                Favorites.TimeStamp = result.TimeStamp;
                
                foreach (var item in result.Documents)
                {
                    documents.Add(item);
                }
            }
         
            return documents;
        }

        public async void RefreshFavorites()
        {
            await Favorites.ClearAndReload();
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
