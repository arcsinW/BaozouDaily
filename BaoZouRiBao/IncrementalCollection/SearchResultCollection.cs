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
    /// <summary>
    /// 搜索结果自增集合
    /// </summary>
    public class SearchResultCollection : IncrementalLoadingBase<Document>
    {
        #region Fields

        private bool hasMore = true;
        private int pageIndex = 1;
        private string keyword;
         
        #endregion

        public void Refresh()
        {
            Clear();
            pageIndex = 1;
            hasMore = true;
        }
         
        public SearchResultCollection(string keyword)
        {
            this.keyword = keyword; 
        }
          
        public SearchResultCollection()
        {

        }

        public async void SetKeyword(string keyword)
        {
            this.keyword = keyword;
            Refresh();
            await LoadMoreItemsAsyncCore(CancellationToken.None, 1);
        }

        protected override bool HasMoreItemsCore
        {
            get
            {
                return hasMore;
            }
        }

        protected override async Task<LoadMoreItemsResult> LoadMoreItemsAsyncCore(CancellationToken cancel, uint count)
        {
            LoadMoreItemsResult result = new LoadMoreItemsResult();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                result.Count = 0;
                return result;
            }
            var searchResult = await ApiService.Instance.SearchAsync(keyword, pageIndex++);
            if (searchResult == null || searchResult.Documents.Length == 0)
            {
                hasMore = false;
                result.Count = 0;
            }
            else
            {
                foreach (var item in searchResult.Documents)
                {
                    Add(item);
                }
                result.Count = (uint)searchResult.Documents.Length;
            }
            return result;
        }
        
    }
}
