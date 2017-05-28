using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{ 
    public sealed partial class DefaultPage : Page
    {
        public DefaultPage()
        {
            this.InitializeComponent();

            //Loaded += DefaultPage_Loaded;
        }

        //private void DefaultPage_Loaded(object sender, RoutedEventArgs e)
        //{
        //    for (int i = 1; i < 6; i++)
        //    {
        //        gifControl.Items.Add(new BitmapImage(new Uri($"ms-appx:///Assets/Images/loadinganim{i}.png")));
        //    }

        //    gifControl.Show();
        //}

        //private void gifControl_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    gifControl.IsActive = !gifControl.IsActive;
        //}
    }
}
