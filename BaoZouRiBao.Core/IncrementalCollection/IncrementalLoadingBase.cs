using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace BaoZouRiBao.Core.IncrementalLoadingCollection
{
    public abstract class IncrementalLoadingBase<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        protected bool busy = false;
        

        public bool HasMoreItems
        {
            get
            {
                return HasMoreItemsCore;
            }
        }

        protected abstract bool HasMoreItemsCore { get; }


        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (!busy)
            {
                busy = true;
                return AsyncInfo.Run(c => LoadMoreItemsAsync(c, count));
            }
            else
            {
                throw new InvalidOperationException("Only one operation in a time");
            }
        }

        protected async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c,uint count)
        {
            try
            {
                this.OnDataLoadingEvent?.Invoke();
                var result = await LoadMoreItemsAsyncCore(c, count);
                
                this.OnDataLoadedEvent?.Invoke();
                return result;
            }
            finally
            {
                busy = false;
            }
        }

        protected virtual void AddItems(IList<T> items)
        {
            if(items!=null)
            {
                foreach (var item in items)
                {
                    this.Add(item);
                }
            }
        }

        private void IncrementalLoadingBase_OnDataLoadingEvent()
        {
            OnDataLoading();
        }

        private void IncrementalLoadingBase_OnDataLoadedEvent()
        {
            OnDataLoaded();
        }

        protected abstract Task<LoadMoreItemsResult> LoadMoreItemsAsyncCore(CancellationToken cancel, uint count);
       

        internal delegate void OnDataLoadedHandler();
        internal delegate void OnDataLoadingHandler();

        internal event OnDataLoadedHandler OnDataLoadedEvent;
        internal event OnDataLoadingHandler OnDataLoadingEvent;

        protected virtual void OnDataLoaded() { }
        protected virtual void OnDataLoading() { }
    }
}
