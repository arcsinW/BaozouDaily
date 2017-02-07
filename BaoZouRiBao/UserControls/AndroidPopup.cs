using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace BaoZouRiBao.UserControls
{
    //[TemplatePart(Name = PopupName, Type = typeof(Popup))]
    [TemplatePart(Name = GridName , Type = typeof(Grid))]
    public sealed class AndroidPopup : ContentControl
    {
        #region Controls's name
        private const string PopupName = "Popup";
        private const string GridName = "BackgroundGrid";
        #endregion

      

        #region Dependency Properties
        //public bool IsOpen
        //{
        //    get { return (bool)GetValue(IsOpenProperty); }
        //    set { SetValue(IsOpenProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsOpenProperty =
        //    DependencyProperty.Register("IsOpen", typeof(bool), typeof(AndroidPopup), new PropertyMetadata(0,OnIsOpenPropertyChanged));

        //private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    AndroidPopup androidPopup = d as AndroidPopup;
        //    if (androidPopup != null)
        //    {
        //        androidPopup.OnIsOpenChanged(e);
        //    }
        //}

        //private void OnIsOpenChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    var popup = GetTemplateChild(PopupName) as Popup;
        //    if (popup != null)
        //    {
        //        _popup = popup;
        //        _popup.IsOpen = (bool)e.NewValue;
        //    }
        //}
        

        #endregion

        public AndroidPopup()
        {
            this.DefaultStyleKey = typeof(AndroidPopup);
           
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
     
        }

        #region Private helpers
        /// <summary>
        /// get screen's height
        /// </summary>
        /// <returns></returns>
        public static double GetApplicationHeight()
        {
            return Window.Current.Bounds.Height;
        }

        /// <summary>
        /// get screen's width
        /// </summary>
        /// <returns></returns>
        public static double GetApplicationWidth()
        {
            return Window.Current.Bounds.Width;
        }
        #endregion
    }
}
