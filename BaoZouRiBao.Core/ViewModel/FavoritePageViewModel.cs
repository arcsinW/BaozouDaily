using BaoZouRiBao.Core.Http;
using BaoZouRiBao.Core.IncrementalCollection;
using BaoZouRiBao.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.ViewModel
{
    public class FavoritePageViewModel : ViewModelBase
    {
        public IncrementalLoadingList<Document> Favorites { get; set; }
     
        public FavoritePageViewModel()
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
    }
}
