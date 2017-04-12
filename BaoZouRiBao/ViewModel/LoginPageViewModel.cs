using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoZouRiBao.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        public void baozouLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.DetailFrameNavigate(typeof(BaozouLoginPage));
        }

        public async void SinaLogin()
        {
            await ApiService.Instance.SinaWeiboLoginAsync();
        }

        public async void QQLogin()
        {
            await ApiService.Instance.TecentLoginAsync();
        }

        public async void WechatLogin()
        {
           
        }
    }
}
