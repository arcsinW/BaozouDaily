using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace BaoZouRiBao.Actions
{
    public class OpenMenuFlyoutAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            var frameworkElement = sender as FrameworkElement;
            var flyoutBase = FlyoutBase.GetAttachedFlyout(frameworkElement);
            flyoutBase.ShowAt(frameworkElement);

            return null;
        }
    }
}
