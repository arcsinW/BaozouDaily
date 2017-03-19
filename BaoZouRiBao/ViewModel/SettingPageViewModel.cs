using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class SettingPageViewModel : ViewModelBase
    {
        public Settings Settings { get; set; } = new Settings();

        public SettingPageViewModel()
        {

        }
    }
}
