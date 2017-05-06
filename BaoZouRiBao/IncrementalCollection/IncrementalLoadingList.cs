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
        /// <summary>
        /// 通过页码增量加载
        /// </summary>
        /// <param name="func">页码增量加载方法</param>
        /// <param name="onDataLoadedAction">数据加载完成</param>
        /// <param name="onDataLoadingAction">数据加载中</param>
        public IncrementalLoadingList(Func<uint, int, Task<IEnumerable<T>>> func, Action onDataLoadedAction = null, Action onDataLoadingAction = null)
        {
            Page = 0;
            this.pageFunc = func;
            this.HasMoreItems = true;

            this.onDataLoadedAction = onDataLoadedAction;
            this.onDataLoadingAction = onDataLoadingAction;
        }

        /// <summary>
        /// 通过时间戳加载
        /// </summary>
        /// <param name="func">通过时间戳加载数据的方法</param>
        /// <param name="onDataLoadedAction">数据加载完成</param>
        /// <param name="onDataLoadingAction">数据加载中</param>
        public IncrementalLoadingList(Func<uint, string, Task<IEnumerable<T>>> func, Action onDataLoadedAction = null, Action onDataLoadingAction = null)
        {
            TimeStamp = string.Empty;
            this.timeStampFunc = func;
            this.HasMoreItems = true;

            this.onDataLoadingAction = onDataLoadingAction;
            this.onDataLoadedAction = onDataLoadedAction;
        }

        #region Fields
        private bool isBusy = false;

        public string TimeStamp { get; set; } = string.Empty;

        // 当前页数
        public int Page { get; private set; }

        /// <summary>
        /// 按页码加载
        /// </summary>
        private Func<uint, int, Task<IEnumerable<T>>> pageFunc;

        /// <summary>
        /// 按时间戳加载
        /// </summary>
        private Func<uint, string, Task<IEnumerable<T>>> timeStampFunc;

        private Action onDataLoadedAction;
        private Action onDataLoadingAction; 
        #endregion

        public bool HasMoreItems
        {
            get; private set;
        }
        
        public async Task ClearAndReloadAsync()
        {
            Clear();
            Page = 0;
            TimeStamp = string.Empty;
            await LoadMoreItemsAsync(0);
        }

        /// <summary>
        /// 没有更多数据
        /// </summary>
        public void NoMore()
        {
            this.HasMoreItems = false;
        }
          
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            onDataLoadingAction?.Invoke();
            return AsyncInfo.Run(async token =>
            {
                try
                {
                    if (isBusy)
                    {
                        
                    }
                    isBusy = true;
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
                        var items = await pageFunc(count, ++Page);
                        foreach (var item in items)
                        {
                            this.Add(item);
                        }
                    }
                    isBusy = false;
                }
                catch (Exception e)
                {
                    LogHelper.WriteLine(e);
                }
                finally
                {
                    NoMore();
                    onDataLoadedAction?.Invoke();
                }

                return new LoadMoreItemsResult { Count = (uint)this.Count };
            });
        }
    }
}
