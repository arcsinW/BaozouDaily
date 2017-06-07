using BaoZouRiBao.Model;
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
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ContributeInChannelPage : Page
    {
        public ContributeInChannelPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var channel = e.Parameter as Channel;
            if(channel !=null)
            {
                titleTextBlock.Text = channel.Name;
                ViewModel.ChannelId = channel.Id;
            }
        }

        private void contributeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var contribute = e.ClickedItem as Contribute;
            if (contribute != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = contribute.Title, DocumentId = contribute.DocumentId, DisplayType = contribute.DisplayType,WebViewUri = contribute.Url };
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
            }
        }    
    }
}
