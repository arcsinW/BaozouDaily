using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml.Data;
using BaoZouRiBao.Http;

namespace BaoZouRiBao.IncrementalCollection
{
    public class ChannelCollection : IncrementalLoadingBase<Channel>
    {
        private int page = 1;
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
            var channels = await ApiService.Instance.GetChannelsAsync(page++);
            if (channels != null && channels.Channels !=null)
            {
                result.Count = (uint)channels.Channels.Length;
                foreach (var item in channels.Channels)
                {
                    Add(item);
                }
            }
            return result;
        }
    }
}
