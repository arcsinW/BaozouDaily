using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class MyRecommendViewModel : ViewModelBase
    {
        private string shareUri = "请先复制你想推荐的链接";
        public string ShareUri
        {
            get
            {
                return shareUri;
            }
            set
            {
                shareUri = value;
                OnPropertyChanged();
            }
        }
    }
}
