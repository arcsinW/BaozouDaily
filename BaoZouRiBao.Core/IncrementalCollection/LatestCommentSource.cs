using BaoZouRiBao.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BaoZouRiBao.Core.Http;
using BaoZouRiBao.Core.Model.ResultModel;

namespace BaoZouRiBao.Core.IncrementalCollection
{
    /// <summary>
    /// Get the latest comment of a essay
    /// </summary>
    public class LatestCommentSource : IIncrementalSource<Comment>
    {
        private string _documentId;
        private StringBuilder _timeStamp = new StringBuilder(string.Empty);

        public LatestCommentSource(string documentId)
        {
            this._documentId = documentId;
        }

        public async Task<IEnumerable<Comment>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken), string timeStamp = "")
        {
            LatestOrHotComment response = await ApiService.Instance.GetLatestOrHotComments(_documentId, Enums.CommentTypeEnum.latest, _timeStamp.ToString());
            if (response == null)
            {
                return null;
            }
            else
            {
                _timeStamp.Clear();
                _timeStamp.Append(response.TimeStamp);
                return response.Comments;
            }
        }

        public void Refresh(object obj)
        {
            _timeStamp.Clear();
        }

        public void Reset()
        {
            _timeStamp.Clear();
        }
    }
}
