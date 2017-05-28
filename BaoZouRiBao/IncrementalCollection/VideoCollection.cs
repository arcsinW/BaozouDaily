using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BaoZouRiBao.IncrementalCollection
{
    public class VideoCollection : IncrementalLoadingBase<Video>
    {
        private StringBuilder timeStamp = new StringBuilder(string.Empty);

        public void Reset()
        {
            timeStamp.Clear();
        }

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
            var videos = await ApiService.Instance.GetLatestVideoAsync(timeStamp.ToString());

            if (videos != null)
            {
                timeStamp.Clear();
                timeStamp.Append(videos.TimeStamp);
                result.Count = (uint)videos.Videos.Count;
                foreach (var item in videos.Videos)
                {
                    Add(item);
                }
 
            }
            return result;
        }
        
    }
}
