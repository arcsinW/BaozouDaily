using BaoZouRiBao.Enums;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
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
    public class CommentCollection : IncrementalLoadingBase<Comment>
    {
        private StringBuilder timeStamp = new StringBuilder(string.Empty);
        private CommentTypeEnum type;
        private string documentId;

        public void Reset()
        {
            timeStamp.Clear();
        }

        public CommentCollection(CommentTypeEnum type,string documentId)
        {
            this.type = type;
            this.documentId = documentId;
        }

        private bool hasMoreItems = true;
        protected override bool HasMoreItemsCore
        {
            get
            {
                return hasMoreItems;
            }
        }

        protected override async Task<LoadMoreItemsResult> LoadMoreItemsAsyncCore(CancellationToken cancel, uint count)
        {
            LoadMoreItemsResult result = new LoadMoreItemsResult();
           
            LatestOrHotComment comments = await ApiService.Instance.GetLatestOrHotCommentsAsync(documentId, type,timeStamp.ToString());
            Debug.WriteLine($"Current TimeStamp : {timeStamp}");
            if (comments != null)
            {
                Debug.WriteLine($"Next TimeStamp : {timeStamp}");
                if (comments.TimeStamp.Equals(timeStamp.ToString()) ||
                    comments.TimeStamp.Equals("0"))
                {
                    hasMoreItems = false;
                }
                timeStamp.Clear();
                timeStamp.Append(comments.TimeStamp);
                result.Count = (uint)comments.Comments.Length;
                foreach (var item in comments.Comments)
                {
                    Add(item);
                }
            }
            return result;
        }
    }
}
