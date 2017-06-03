using BaoZouRiBao.Http;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class DownloadOfflinePageViewModel : ViewModelBase
    {
        public DownloadOfflinePageViewModel()
        {

        }

        /// <summary>
        /// 离线下载
        /// </summary>
        public async void Download()
        {
            OfflineDownloadResult result = await ApiService.Instance.OfflineDownloadAsync();
        }
    }
}
