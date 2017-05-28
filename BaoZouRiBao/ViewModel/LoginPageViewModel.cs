using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.Controls;
using BaoZouRiBao.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using BaoZouRiBao.Model.ResultModel;

namespace BaoZouRiBao.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        public void baozouLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.DetailFrameNavigate(typeof(BaozouLoginPage));
        }

        /// <summary>
        /// 新浪微博授权登录
        /// </summary>
        public async void SinaLogin()
        {
            User user = await ApiService.Instance.SinaWeiboLoginAsync();
            if (user != null)
            {
                DataShareManager.Current.UpdateUser(user);
                ToastService.SendToast("登录成功");
            }
            else
            {
                ToastService.SendToast("登录失败");
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

        public void WechatLogin()
        {
           
        }
    }
}
