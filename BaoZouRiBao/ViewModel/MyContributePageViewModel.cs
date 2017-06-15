using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class MyContributePageViewModel : ViewModelBase
    {
        public MyContributePageViewModel()
        {
            Contributes = new IncrementalLoadingList<Contribute>(GetContributes, () => { IsActive = false; }, () => { IsActive = true; });

            if (IsDesignMode)
            {
                LoadDesignData();
            }
        }
         
        #region Properties
        public IncrementalLoadingList<Contribute> Contributes { get; set; }

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

        public void LoadDesignData()
        {
            string json = "{\"data\":[{\"title\":\"大连女孩卧底微信群揭开“五行币”传销内幕_腾讯网触屏版\",\"comment_count\":0,\"vote_count\":0,\"contribute\":1,\"timestamp\":1492353355,\"url\":\"http://xw.qq.com/ln/20170414007169/LNC2017041400716900\",\"source_name\":\"\",\"hit_count\":0,\"hit_count_string\":\"0\",\"publish_time\":1492175420000,\"published_at\":\"2017-04-14 21:10\",\"recommenders\":[{\"id\":720554,\"name\":\"arcsinw\",\"avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\"}],\"thumbnail\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\",\"author_id\":720554,\"author_name\":\"arcsinw\",\"author_avatar\":\"http://wanzao2.b0.upaiyun.com/baozouribao/59d06920034601356a05525400063398.png\",\"favorited\":false,\"voted\":false,\"commented\":false,\"reason_text\":\"\",\"document_id\":45405,\"display_type\":2,\"visiable\":false,\"status\":0,\"created_at\":\"2017-04-14 21:10\",\"created_time\":1492175420000,\"current_version\":2}],\"timestamp\":1492175420}";
            var result = Helper.JsonHelper.Deserlialize<LatestContributeResult>(json);
            if (result != null && result.Contributes != null)
            {
                foreach (var item in result.Contributes)
                {
                    Contributes.Add(item);
                }
            } 
        }

        public async Task<IEnumerable<Contribute>> GetContributes(uint count, string timeStamp)
        {
            List<Contribute> contributes = new List<Contribute>();

            if (timeStamp.Equals("0"))
            {
                Contributes.NoMore();
                return contributes;
            }

            var result = await ApiService.Instance.GetMyContributeAsync(timeStamp);
            if (result != null && result.Contributes != null)
            {
                Contributes.TimeStamp = result.TimeStamp;

                foreach (var item in result.Contributes)
                {
                    contributes.Add(item);
                }
            } 

            if (contributes.Count == 0 && Contributes.Count == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
            }

            return contributes;
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        public override async void Refresh()
        {
            await Contributes.ClearAndReloadAsync();
        }
    }
}
