using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace BaoZouRiBao.ViewModel
{
    public class MyCommentViewModel : ViewModelBase
    {
        public MyCommentViewModel()
        {
            Comments = new IncrementalLoadingList<Comment>(GetComments,()=> { IsActive = false; },()=> { IsActive = true; });
            if (IsDesignMode)
            {
                LoadDesignTimeData();
            }
        }

        #region Properties
        public IncrementalLoadingList<Comment> Comments { get; set; }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        }

        private bool isEmpty = false;

        public bool IsEmpty
        {
            get { return isEmpty; }
            set { isEmpty = value; OnPropertyChanged(); }
        }

        #endregion

        public void LoadDesignTimeData()
        {
            string json = "[{\"article\":{\"display_type\":\"1\",\"document_id\":\"41889\",\"thumbnail\":\"http://ww1.sinaimg.cn/large/006e3HX0jw1fbx5kkriq5j303c03ca9z.jpg\",\"title\":\"解读《降临》：在时间的长河里衔住自己的尾巴\",\"visiable\":\"true\"},\"content\":\"吐槽\",\"dislike\":\"false\",\"dislikes\":\"0\",\"hottest\":\"false\",\"id\":\"3335744\",\"like\":\"false\",\"likes\":\"0\",\"own\":\"true\",\"parent\":null,\"readable_time\":\"2017-02-06T00:57:17+08:00\",\"score\":\"0\",\"time\":1486313837000,\"user\":{\"id\":\"720554\",\"name\":\"arcsinw\",\"real_avatar_url\":\"http://wanzao2.b0.upaiyun.com/baozouribao/3a31a730cdf3013491d6525400071796.png\"}},{\"article\":{\"display_type\":\"1\",\"document_id\":\"28597\",\"thumbnail\":\"http://ww1.sinaimg.cn/large/005OoJvSjw1f533jb9o1aj303c03cglm.jpg\",\"title\":\"小G娜吴亦凡：如果你想当网红，真的可以选择和偶像约个P！\",\"visiable\":\"true\"},\"content\":\"test\",\"dislike\":\"false\",\"dislikes\":\"0\",\"hottest\":\"false\",\"id\":\"2587581\",\"like\":\"false\",\"likes\":\"0\",\"own\":\"true\",\"parent\":null,\"readable_time\":\"2016-06-21T22:30:48+08:00\",\"score\":\"0\",\"time\":1466519448000,\"user\":{\"id\":\"720554\",\"name\":\"arcsinw\",\"real_avatar_url\":\"http://wanzao2.b0.upaiyun.com/baozouribao/3a31a730cdf3013491d6525400071796.png\"}},{\"article\":{\"display_type\":\"1\",\"document_id\":\"27965\",\"thumbnail\":\"http://ww4.sinaimg.cn/large/005PbSmyjw1f4y35yqitvj303c03ct8j.jpg\",\"title\":\"被切成250段还能存活的动物，好可怕？\",\"visiable\":\"true\"},\"content\":\"test\",\"dislike\":\"false\",\"dislikes\":\"0\",\"hottest\":\"false\",\"id\":\"2570762\",\"like\":\"false\",\"likes\":\"0\",\"own\":\"true\",\"parent\":null,\"readable_time\":\"2016-06-17T21:29:40+08:00\",\"score\":\"0\",\"time\":1466170180000,\"user\":{\"id\":\"720554\",\"name\":\"arcsinw\",\"real_avatar_url\":\"http://wanzao2.b0.upaiyun.com/baozouribao/3a31a730cdf3013491d6525400071796.png\"}}]";
            var comments = Helper.JsonHelper.Deserlialize<List<Comment>>(json);
            foreach (var item in comments)
            {
                Comments.Add(item);
            }
        }

        private async Task<IEnumerable<Comment>> GetComments(uint count,string timeStamp)
        {
            List<Comment> comments = new List<Comment>();
            if(timeStamp.Equals("0"))
            {
                Comments.NoMore();
                return comments;
            }
            var result = await ApiService.Instance.GetMyCommentsAsync(timeStamp);
            if (result != null && result.Comments != null)
            {
                Comments.TimeStamp = result.TimeStamp;
                foreach (var item in result.Comments)
                {
                    comments.Add(item);
                }
            }

            if (comments.Count == 0 && Comments.Count == 0)
            {
                IsEmpty = true;
            }

            return comments;
        }

        public async void RefreshComments()
        {
            await Comments.ClearAndReload();
        }
         
    }
}
