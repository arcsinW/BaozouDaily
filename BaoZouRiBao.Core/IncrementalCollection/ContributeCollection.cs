using BaoZouRiBao.Core.Helper;
using BaoZouRiBao.Core.Http;
using BaoZouRiBao.Core.IncrementalLoadingCollection;
using BaoZouRiBao.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BaoZouRiBao.Core.IncrementalCollection
{
    public class ContributeCollection : IncrementalLoadingBase<Contribute>
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
            var contributes = await ApiService.Instance.GetLatestContribute(timeStamp.ToString());

            if (contributes != null)
            {
                Debug.WriteLine(contributes.TimeStamp);
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
