using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Utils
{
    /// <summary>
    /// 增加导航动画的页面基类
    /// </summary>
    public abstract class AnimatedPage : Page
    {
        PaneThemeTransition PaneAnim = new PaneThemeTransition { Edge = EdgeTransitionLocation.Right };
        public AnimatedPage() : base()
        {
            ManipulationCompleted += AppleAnimationPage_ManipulationCompleted;
            Transitions = new TransitionCollection();
            Transitions.Add(PaneAnim);
            ManipulationMode = ManipulationModes.TranslateX;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PaneAnim.Edge = e.NavigationMode == NavigationMode.Back ? EdgeTransitionLocation.Left : EdgeTransitionLocation.Right;
            base.OnNavigatedTo(e);
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            PaneAnim.Edge = e.NavigationMode != NavigationMode.Back ? EdgeTransitionLocation.Left : EdgeTransitionLocation.Right;
            base.OnNavigatingFrom(e);
        }
        private void AppleAnimationPage_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var trans = e.Cumulative.Translation;
            double DeltaX = Math.Abs(trans.X);
            if (Math.Abs(trans.Y) * 3 < DeltaX && DeltaX > ActualWidth / 2)
            {
                if (trans.X > 0)
                {
                    if (Frame.CanGoBack)
                        Frame.GoBack();
                }
                else
                {
                    if (Frame.CanGoForward)
                        Frame.GoForward();
                }
            }
        }
    }
}
