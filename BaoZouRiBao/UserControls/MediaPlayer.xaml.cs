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
    public sealed partial class MediaPlayer : UserControl
    {
        public MediaPlayer()
        {
            this.InitializeComponent();
        }

        private void MediaElement_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (transportControls.Visibility == Visibility.Visible)
            {
                transportControls.Visibility = Visibility.Collapsed;
            }
            else
            {
                transportControls.Visibility = Visibility.Visible;
            }
        }

        private void transportControls_Tapped(object sender, TappedRoutedEventArgs e)
        {
            e.Handled = true; 

            if (transportControls.Visibility == Visibility.Visible)
            {
                transportControls.Visibility = Visibility.Collapsed;
            }
        }

        private void transportControls_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void MediaElement_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
