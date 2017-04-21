﻿using System;
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
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();

            MasterDetailPage.Current.AdaptiveVisualStateChanged += Current_AdaptiveVisualStateChanged;
        }
         

        protected override void OnKeyUp(KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ViewModel.Search();
            }
        }

        private void Current_AdaptiveVisualStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            switch (e.NewState.Name)
            {
                case "Narrow":
                    splitViewButton.Visibility = Visibility.Visible;
                    //Grid.SetColumn(searchTextBox, 1);
                    break;
                case "Wide":
                    splitViewButton.Visibility = Visibility.Collapsed;
                    //Grid.SetColumn(searchTextBox, 0);
                    //Grid.SetColumnSpan(searchTextBox, 2);
                    break;
            }
        }
    }
}
