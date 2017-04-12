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
    public class ContributeInChannelCollection : IncrementalLoadingBase<Contribute>
    {
        private StringBuilder timeStamp = new StringBuilder(string.Empty);
        private string id;

        public ContributeInChannelCollection(string id)
        {
            this.id = id;
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

            var contributes = await ApiService.Instance.GetContributeInChannelAsync(id,timeStamp.ToString());

            if (contributes != null)
            {
                timeStamp.Clear();
                timeStamp.Append(contributes.TimeStamp);
                result.Count = (uint)contributes.Contributes.Length;
                foreach (var item in contributes.Contributes)
                {
                    Add(item);
                }
            }
            return result;
        }
    }
}
