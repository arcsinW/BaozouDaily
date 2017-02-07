using BaoZouRiBao.Common;
using BaoZouRiBao.Core.Helper;
using BaoZouRiBao.Core.Http;
using BaoZouRiBao.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoZouRiBao.Core.ViewModel
{
    public class BaozouLoginViewModel : ViewModelBase
    {
        public BaozouLoginViewModel()
        {
            Input = new BaozouLoginInput()
            {
                Account = "arcsinw",
                Password = "qwertyx123",
            };
            LoginCommand = new RelayCommand((input) =>
            {
                Login((BaozouLoginInput)input);
            }); 
        }

        public BaozouLoginInput Input { get; set; }


        public RelayCommand LoginCommand { get; set; }

        private async void Login(BaozouLoginInput input)
        {
            await ApiService.Instance.Login(input.Account, input.Password);
        }
    }
}
