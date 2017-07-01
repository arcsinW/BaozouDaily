using BaoZouRiBao.Controls;
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


namespace BaoZouRiBao.UserControls
{
    public sealed partial class ShareDialog : GlobalDialog
    {
        public ShareDialog()
        {
            this.InitializeComponent();
        }

        public event RoutedEventHandler WechatClick;
        public event RoutedEventHandler WeiboClick;
        public event RoutedEventHandler LinkClick;
        public event RoutedEventHandler MoreClick;

        private void weiboBtn_Click(object sender, RoutedEventArgs e)
        {
            WeiboClick?.Invoke(sender, e);
        }       

        private void weChatBtn_Click(object sender, RoutedEventArgs e)
        {
            WechatClick?.Invoke(sender, e);
        }

        private void linkBtn_Click(object sender, RoutedEventArgs e)
        {
            LinkClick?.Invoke(sender, e);
        }

        private void moreBtn_Click(object sender, RoutedEventArgs e)
        {
            MoreClick?.Invoke(sender, e);
        }
    }
}
