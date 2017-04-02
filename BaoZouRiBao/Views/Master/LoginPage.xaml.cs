using BaoZouRiBao.Helper;
using BaoZouRiBao.Http;
using BaoZouRiBao.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void baozouLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.DetailFrameNavigate(typeof(BaozouLoginPage));
        }

        public async void SinaLogin()
        {
            await ApiService.Instance.SinaWeiboLogin();
            
        }

        public async void QQLogin()
        {
            await ApiService.Instance.TecentLogin();
        }

        public void SwitchElementTheme()
        {
            GlobalValue.Current.SwitchElementTheme();
        }
    }
}
