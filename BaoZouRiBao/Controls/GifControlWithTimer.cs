using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace BaoZouRiBao.Controls
{
    public sealed class GifControlWithTimer : Control
    {
        #region Constructor
        public GifControlWithTimer()
        {
            this.DefaultStyleKey = typeof(GifControlWithTimer);
        }

        ~GifControlWithTimer()
        {
            Items = null;
            timer.Tick -= Timer_Tick;
        }
        #endregion

        #region Properties
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(GifControlWithTimer), new PropertyMetadata(new BitmapImage(new Uri("ms-appx:///Assets/Images/loadinganim0.png"))));
        

        public List<BitmapImage> Items { get; set; } = new List<BitmapImage>();


        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(GifControlWithTimer), new PropertyMetadata(true, propertyChangedCallback));

        private static void propertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GifControlWithTimer;
            if ((bool)e.NewValue)
            {
                control.Show();
            }
            else
            {
                control.Stop();
            }
        }
        
        public int Fps
        {
            get { return (int)GetValue(FpsProperty); }
            set
            {
                if (value < 0)
                {
                    SetValue(FpsProperty, 6);
                }
                else
                {
                    SetValue(FpsProperty, value);
                }
            }
        }

        // Using a DependencyProperty as the backing store for Fps.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FpsProperty =
            DependencyProperty.Register("Fps", typeof(int), typeof(GifControlWithTimer), new PropertyMetadata(6));

        
        #endregion

        private int currentIndex = 0;

        private DispatcherTimer timer = new DispatcherTimer();

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            timer.Interval = TimeSpan.FromSeconds( 1.0 / Fps);
            timer.Tick += Timer_Tick;
        }

        private async void Timer_Tick(object sender, object e)
        {
            if (Items.Count == 0)
            {
                IsActive = false;
                return;
            }
            else
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                 {
                     Source = Items[currentIndex];
                 });

                IsActive = true;
            }
            ++currentIndex;

            currentIndex = currentIndex >= Items.Count ? 0 : currentIndex;

            Debug.WriteLine(currentIndex);
        }

        public void Show()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        } 
    }
}
