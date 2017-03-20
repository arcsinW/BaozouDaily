using BaoZouRiBao.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace BaoZouRiBao.IncrementalCollection
{
    /// <summary>
    /// 增量加载集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IncrementalLoadingList<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private bool _isBusy = false;
        public string TimeStamp {set; get; } = string.Empty;
        //当前页数
        public int Page { private set; get; }
        private Func<uint, int, Task<IEnumerable<T>>> pageFunc;
        private Func<uint, string, Task<IEnumerable<T>>> timeStampFunc;


        private Action onDataLoadedAction;
        private Action onDataLoadingAction;

        public bool HasMoreItems
        {
            get; private set;
        }

        /// <summary>
        /// 通过页码增量加载
        /// </summary>
        /// <param name="func"></param>
        public IncrementalLoadingList(Func<uint, int, Task<IEnumerable<T>>> func, Action onDataLoadedAction = null, Action onDataLoadingAction = null)
        {
            Page = 0;
            this.pageFunc = func;
            this.HasMoreItems = true;

            this.onDataLoadedAction = onDataLoadedAction;
            this.onDataLoadingAction = onDataLoadingAction;
        }

        /// <summary>
        /// 通过时间戳增量加载
        /// </summary>
        /// <param name="func"></param>
        public IncrementalLoadingList(Func<uint,string,Task<IEnumerable<T>>> func, Action onDataLoadedAction = null, Action onDataLoadingAction = null)
        {
            TimeStamp = string.Empty;
            this.timeStampFunc = func;
            this.HasMoreItems = true;

            this.onDataLoadingAction = onDataLoadingAction;
            this.onDataLoadedAction = onDataLoadedAction;
        }

        public async Task ClearAndReload()
        {
            Clear();
            Page = 0;
            TimeStamp = string.Empty;
            await LoadMoreItemsAsync(0);
        }

        public void NoMore()
        {
            this.HasMoreItems = false;
        }

        public void HasMore()
        {
            this.HasMoreItems = true;
        }
      
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            onDataLoadingAction?.Invoke();
            return AsyncInfo.Run(async token =>
            {
                try
                {
                    if (_isBusy)
                    {
                        throw new InvalidOperationException("busy now");
                    }
                    _isBusy = true;
                    if (timeStampFunc != null)
                    {
                        var _items = await timeStampFunc(count, TimeStamp);
                        foreach (var item in _items)
                        {
                            this.Add(item);
                        }
                    }
                    else
                    {
                        var _items = await pageFunc(count, ++Page);
                        foreach (var item in _items)
                        {
                            this.Add(item);
                        }
                    }
                    _isBusy = false;
                }
                catch(Exception e)
                {
                    LogHelper.WriteLine(e);
                }
                finally
                {
                    onDataLoadedAction?.Invoke();
                }

                return new LoadMoreItemsResult { Count = (uint)this.Count };
            });
        }
    }
}
