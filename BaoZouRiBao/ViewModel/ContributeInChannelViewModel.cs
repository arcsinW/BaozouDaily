using BaoZouRiBao.Controls;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class ContributeInChannelViewModel : ViewModelBase
    {
        public ContributeInChannelViewModel()
        {
            if (IsDesignMode)
            {
                ChannelId = "3";
            }

            Contributes = new IncrementalLoadingList<Contribute>(LoadContributesAsync, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(e.Message); IsActive = false; });
        }

        private StringBuilder timeStampStringBuilder = new StringBuilder("0");

        /// <summary>
        /// 按时间戳加载频道内数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Contribute>> LoadContributesAsync(uint count, string timeStamp)
        {
            if (timeStamp.Equals(timeStampStringBuilder.ToString()))
            {
                Contributes.NoMore();
                return null;
            }

            var result = await ApiService.Instance.GetContributeInChannelAsync(ChannelId, timeStamp);

            if (result != null && result.Contributes != null)
            {
                List<Contribute> contributes = new List<Contribute>();

                timeStampStringBuilder.Clear();
                timeStampStringBuilder.Append(timeStamp);

                Contributes.TimeStamp = result.TimeStamp;

                result.Contributes?.ForEach(x => contributes.Add(x));

                return contributes;
            }
            else
            {
                Contributes.NoMore();

                if (Contributes.Count == 0)
                {
                    IsEmpty = true;
                }
            }

            return null;
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

        /// <summary>
        /// 频道Id
        /// </summary>
        public string ChannelId { get; set; }
          
        /// <summary>
        /// 刷新
        /// </summary>
        public override async void Refresh()
        {
            await Contributes.ClearAndReloadAsync();
        }
    }
}
