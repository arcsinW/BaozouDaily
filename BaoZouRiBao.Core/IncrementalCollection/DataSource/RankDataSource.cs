using BaoZouRiBao.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BaoZouRiBao.Core.Enums;
using BaoZouRiBao.Core.Http;

namespace BaoZouRiBao.Core.IncrementalCollection.DataSource
{
    public class RankDataSource : IIncrementalSource<Document>
    {
        private RankTypeEnum _type;
        private RankTimeEnum _time;
        public RankDataSource(RankTypeEnum type,RankTimeEnum time)
        {
            this._type = type;
            this._time = time;
        }

        public RankDataSource()
        {

        }

        public async Task<IEnumerable<Document>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken), string timeStamp = "")
        {
            LatestDocumentResult result = await ApiService.Instance.GetRank(_type, _time);
            
            return result?.Data;
        } 

        public async void Refresh(object obj)
        {
            if (obj != null)
            {
                RankTimeEnum time = (RankTimeEnum)obj;
                _time = time;
            }
            await GetPagedItemsAsync(0, 0);
        } 
    }
}
