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
 
namespace BaoZouRiBao.Controls
{
    public sealed class CommentControl : Control
    {
        public CommentControl()
        {
            this.DefaultStyleKey = typeof(CommentControl);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #region Properties
        public RoutedEventHandler ReplyClick
        {
            get { return (RoutedEventHandler)GetValue(ReplyClickProperty); }
            set { SetValue(ReplyClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReplyClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReplyClickProperty =
            DependencyProperty.Register("ReplyClick", typeof(RoutedEventHandler), typeof(CommentControl), new PropertyMetadata(0)); 
        #endregion

    }
}
