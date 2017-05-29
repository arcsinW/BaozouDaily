using BaoZouRiBao.Helper;
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
    public sealed partial class MessagePage : Page
    {
        public MessagePage()
        {
            this.InitializeComponent();
        }

        public async void Refresh()
        {
            switch (pivot.SelectedIndex)
            {
                case 0:
                    await ViewModel.RefreshCommentMessagesAsync();
                    break;
                case 1:
                    await ViewModel.RefreshVoteMessagesAsync();
                    break;
                case 2:
                    await ViewModel.RefreshAdminMessagesAsync();
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Message message = ((Button)sender)?.DataContext as Message;

            if (message != null && message.ReceiverComment != null)
            {
                var article = message.ReceiverComment.Article;
                if (article != null)
                {
                    if (!article.DisplayType.Equals("3"))
                    {
                        WebViewParameter parameter = new WebViewParameter() { Title = article.Title, DocumentId = article.DocumentId, DisplayType = "1" };
                        MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
                    }
                    else
                    {
                        NavigationHelper.DetailFrameNavigate(typeof(VideoPage), article.DocumentId);
                    }
                }
            }
        }
    }
}
