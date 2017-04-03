using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;

namespace BaoZouRiBao.ViewModel
{
    public class ChannelViewModel : ViewModelBase
    {
        public ChannelViewModel()
        {
            Channels = new IncrementalLoadingList<Channel>(GetChannel);
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        }

        public int TotalPages { get; set; } = -1;

        public IncrementalLoadingList<Channel> Channels { get; set; }

        private async Task<IEnumerable<Channel>> GetChannel(uint count,int pageIndex)
        {
            IsActive = true;
            List<Channel> channels = new List<Channel>();
            if (TotalPages > 0 && pageIndex > TotalPages)
            {
                Channels.NoMore();
                IsActive = false;
                return channels;
            }

            var result = await ApiService.Instance.GetChannels(pageIndex++);

            if (result != null && result.Channels != null)
            {
                TotalPages = result.TotalPages;
                return result.Channels;
            }

            IsActive = false;
            return channels;
        }
    }
}
