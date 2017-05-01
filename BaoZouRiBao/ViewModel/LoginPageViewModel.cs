using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.UserControls;
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
             bool result = await ApiService.Instance.SinaWeiboLoginAsync();
            if (result)
            {
                ToastService.SendToast("登录成功");
            }
        }

        public async void QQLogin()
        {
            bool result = await ApiService.Instance.TecentLoginAsync();
            if (result)
            {
                ToastService.SendToast("登录成功");
            }
        }

        public async void WechatLogin()
        {
           
        }
    }
}
