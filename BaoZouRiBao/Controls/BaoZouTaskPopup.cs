using BaoZouRiBao.Enums;
using BaoZouRiBao.Helper;
using System;
using System.Collections.Generic;
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
    public sealed class BaoZouTaskPopup : Control
    {
        public BaoZouTaskPopup()
        {
            this.DefaultStyleKey = typeof(BaoZouTaskPopup);
        }

        #region Fields
        const string IMAGE = "image";

         Image image = null;
        #endregion

        #region Dependency Properties
        public BaoZouTaskEnum BaoZouPopupType
        {
            get { return (BaoZouTaskEnum)GetValue(PopupTypeProperty); }
            set { SetValue(PopupTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupTypeProperty =
            DependencyProperty.Register("PopupType", typeof(BaoZouTaskEnum), typeof(BaoZouTaskPopup), new PropertyMetadata(0));

        public string CoinCount
        {
            get { return (string)GetValue(CoinCountProperty); }
            set { SetValue(CoinCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CoinCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoinCountProperty =
            DependencyProperty.Register("CoinCount", typeof(string), typeof(BaoZouTaskPopup), new PropertyMetadata(0));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(BaoZouTaskPopup), new PropertyMetadata(0));

        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(string), typeof(BaoZouTaskPopup), new PropertyMetadata(0));
         
        public Uri UriSource
        {
            get { return (Uri)GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UriSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UriSourceProperty =
            DependencyProperty.Register("UriSource", typeof(Uri), typeof(BaoZouTaskPopup), new PropertyMetadata(0, propertyChangedCallback));

        private static void propertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BaoZouTaskPopup popup =  d as BaoZouTaskPopup;
            if(popup!=null)
            {
                popup.image.Source = new BitmapImage() { UriSource = (Uri)e.NewValue };
            }
        } 
        
         
        #endregion


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            image = GetTemplateChild(IMAGE) as Image;

            if(image!=null)
            {
                UriSource = new Uri("ms-appx:///Assets/Images/img_popout_box_" + BaoZouPopupType.ToString() + ".png");
            }
        }
    }
}
