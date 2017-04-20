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
    public sealed partial class VideoPage : Page
    {
        public VideoPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string documentId = e.Parameter as string;

            if(string.IsNullOrEmpty(documentId))
            {
                ViewModel.LoadData(documentId);
            }
        }

        private void Comment_Click(object sender, RoutedEventArgs e)
        {
            NavigationHelper.DetailFrameNavigate(typeof(CommentPage), ViewModel.Video.DocumentId);
        }
    }
}
