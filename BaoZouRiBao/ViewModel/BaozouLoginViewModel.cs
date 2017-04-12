using BaoZouRiBao.Common;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoZouRiBao.ViewModel
{
    public class BaozouLoginViewModel : ViewModelBase
    {
        public BaozouLoginViewModel()
        {
            Input = new BaozouLoginInput()
            {
#if DEBUG
                Account = "arcsinw",
                Password = "qwertyx123",
#endif
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
            await ApiService.Instance.LoginAsync(input.Account, input.Password);
        }
    }
}
