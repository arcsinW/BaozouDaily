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
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();

            MasterDetailPage.Current.AdaptiveVisualStateChanged += Current_AdaptiveVisualStateChanged;
        }

        private void Current_AdaptiveVisualStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            switch (e.NewState.Name)
            {
                case "Narrow":
                    splitViewButton.Visibility = Visibility.Collapsed;
                    pageTitleTextBlock.Margin = new Thickness(4, 0, 4, 0);
                    break;
                case "Wide":
                    splitViewButton.Visibility = Visibility.Visible;
                    pageTitleTextBlock.Margin = new Thickness(12, 0, 12, 0);
                    break;
            }
        }
    }
}
