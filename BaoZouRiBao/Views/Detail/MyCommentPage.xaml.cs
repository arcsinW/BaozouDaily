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
    public sealed partial class MyCommentPage : Page
    {
        public MyCommentPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Comment comment = ((Button)sender)?.DataContext as Comment;
            
            if (comment != null && comment.Article != null)
            {
                if (!comment.Article.DisplayType.Equals("3"))
                {
                    WebViewParameter parameter = new WebViewParameter() { Title = "", DocumentId = comment.Article.DocumentId, DisplayType = "1" };
                    MasterDetailPage.Current.DetailFrame.Navigate(typeof(WebViewPage), parameter);
                }
                else
                {
                    NavigationHelper.DetailFrameNavigate(typeof(VideoPage), comment.Article.DocumentId);
                }   
            }
        }
    }
}
