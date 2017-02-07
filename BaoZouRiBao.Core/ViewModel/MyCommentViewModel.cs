using BaoZouRiBao.Core.Http;
using BaoZouRiBao.Core.IncrementalCollection;
using BaoZouRiBao.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BaoZouRiBao.Core.ViewModel
{
    public class MyCommentViewModel : ViewModelBase
    {
        public IncrementalLoadingList<Comment> Comments { get; set; }

        public MyCommentViewModel()
        {
            Comments = new IncrementalLoadingList<Comment>(GetComments);
        }

        private async Task<IEnumerable<Comment>> GetComments(uint count,string timeStamp)
        {
            List<Comment> comments = new List<Comment>();
            if(timeStamp.Equals("0"))
            {
                Comments.NoMore();
                return comments;
            }
            var result = await ApiService.Instance.GetMyComments(timeStamp);
            if (result != null && result.Comments != null)
            {
                Comments.TimeStamp = result.TimeStamp;
                foreach (var item in result.Comments)
                {
                    comments.Add(item);
                }
            }
            return comments;
        }

        public async void RefreshComments()
        {
            await Comments.ClearAndReload();
        }
    }
}
