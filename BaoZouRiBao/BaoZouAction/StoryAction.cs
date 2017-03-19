using BaoZouRiBao.Model;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Views;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoZouRiBao.BaoZouAction
{
    public class StoryAction : DependencyObject, IAction
    {
        public Document Story
        {
            get { return (Document)GetValue(StoryProperty); }
            set { SetValue(StoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Story.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StoryProperty =
            DependencyProperty.Register("Story", typeof(string), typeof(Document), new PropertyMetadata(0));


        public object Execute(object sender, object parameter)
        { 
            if (Story != null)
            {
                WebViewParameter para = new WebViewParameter() { Title = "", WebViewUri = Story.Url, Data = Story };
                NavigationHelper.DetailFrameNavigate(typeof(WebViewPage), para);
            }
            return true;
        }
    }
}
