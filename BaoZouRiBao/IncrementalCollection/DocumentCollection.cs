
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml.Data;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BaoZouRiBao.IncrementalCollection
{
    public class DocumentCollection : IncrementalLoadingBase<Document>
    {
        private StringBuilder timeStamp = new StringBuilder(string.Empty);

        public void Reset()
        {
            timeStamp.Clear();
        }

        public DocumentCollection()
        {
            TopStories = new ObservableCollection<Document>();           
        }

        public ObservableCollection<Document> TopStories { get; set; }

        protected override bool HasMoreItemsCore
        {
            get
            {
                return true;
            }
        }

        protected override async Task<LoadMoreItemsResult> LoadMoreItemsAsyncCore(CancellationToken cancel, uint count)
        {
            LoadMoreItemsResult result = new LoadMoreItemsResult();
            var stories = await ApiService.Instance.GetLatestDocumentAsync(timeStamp.ToString());

            if (stories != null)
            {
                timeStamp.Clear();
                timeStamp.Append(stories.TimeStamp);

                result.Count = (uint)stories.Data.Count;
                foreach (var item in stories.Data)
                {
                    Add(item);
                }
                if (stories.TopStories != null)
                {
                    foreach (var item in stories.TopStories)
                    {
                        TopStories.Add(item);
                    }
                }
            }
            return result;
        }
        
    }
}
