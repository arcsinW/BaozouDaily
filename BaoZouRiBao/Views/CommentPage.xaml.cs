using BaoZouRiBao.Core.ViewModel;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace BaoZouRiBao.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CommentPage : Page
    {
        public CommentPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string documentId = (string)e.Parameter;
            if(!string.IsNullOrEmpty(documentId))
            {
                ViewModel.SetDocumentId(documentId);               
            }
        }
         
        private bool isHotLoaded = false;
        private bool isLatestLoaded = false;

        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    if(!isLatestLoaded)
                    {
                        ViewModel.GetLatestComments();
                        isLatestLoaded = true;
                    }
                    break;
                case 1:
                    if(!isHotLoaded)
                    {
                        ViewModel.GetHotComments();
                        isHotLoaded = true;
                    }
                    break;
            }
        }
    }
}
