using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BaoZouRiBao.Extension
{
    public class WebViewExtension
    {
        public static readonly DependencyProperty HtmlStringProperty =
           DependencyProperty.RegisterAttached("HtmlString", typeof(string), typeof(WebViewExtension), new PropertyMetadata("", OnHtmlStringChanged));

     
        public static string GetHtmlString(DependencyObject obj) { return (string)obj.GetValue(HtmlStringProperty); }
        public static void SetHtmlString(DependencyObject obj, string value) { obj.SetValue(HtmlStringProperty, value); }

     
        private static void OnHtmlStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebView wv = d as WebView;
            wv?.NavigateToString((string)e.NewValue);
        }
    }
}
