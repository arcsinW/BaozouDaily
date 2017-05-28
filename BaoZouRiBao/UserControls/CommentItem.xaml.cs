using BaoZouRiBao.Common;
using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.UserControls
{
    public sealed partial class CommentItem : UserControl
    {
        public CommentItem()
        {
            this.InitializeComponent();
        }

        #region Properties
        /// <summary>
        /// 点赞 Command
        /// </summary>
        public ICommand VoteCommand
        {
            get { return (ICommand)GetValue(VoteCommandProperty); }
            set { SetValue(VoteCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VoteCommandProperty =
            DependencyProperty.Register("VoteCommand", typeof(int), typeof(CommentItem), new PropertyMetadata(0));

        public object VoteCommandParameter
        {
            get { return (object)GetValue(VoteCommandParameterProperty); }
            set { SetValue(VoteCommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VoteCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VoteCommandParameterProperty =
            DependencyProperty.Register("VoteCommandParameter", typeof(object), typeof(CommentItem), new PropertyMetadata(0));

        /// <summary>
        /// 评论 Command
        /// </summary>
        public ICommand CommentCommand
        {
            get { return (ICommand)GetValue(CommentCommandProperty); }
            set { SetValue(CommentCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommentCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommentCommandProperty =
            DependencyProperty.Register("CommentCommand", typeof(ICommand), typeof(CommentItem), new PropertyMetadata(0));

        public object CommentCommandParamter
        {
            get { return (object)GetValue(CommentCommandParamterProperty); }
            set { SetValue(CommentCommandParamterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommentCommandParamter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommentCommandParamterProperty =
            DependencyProperty.Register("CommentCommandParamter", typeof(object), typeof(CommentItem), new PropertyMetadata(0));
        
        public object CommentId
        {
            get { return (object)GetValue(CommentIdProperty); }
            set { SetValue(CommentIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommentId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommentIdProperty =
            DependencyProperty.Register("CommentId", typeof(object), typeof(CommentItem), new PropertyMetadata(0));



        public RoutedEventHandler ReplyClick
        {
            get { return (RoutedEventHandler)GetValue(ReplyClickProperty); }
            set { SetValue(ReplyClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReplyClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReplyClickProperty =
            DependencyProperty.Register("ReplyClick", typeof(RoutedEventHandler), typeof(CommentItem), new PropertyMetadata(0));


        #endregion

        private void replyBtn_Click(object sender, RoutedEventArgs e)
        {
            ReplyClick?.Invoke(sender,e);
        }

        private void replyFlyout_Click(object sender, RoutedEventArgs e)
        {
            ReplyClick?.Invoke(sender,e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var comment = (sender as CommentItem).DataContext as Comment;

            if (comment != null)
            {
                if (comment.Parent != null)
                {
                    parentTextBlock.Inlines.Add(new Run() { Text = "回复" });

                    Run link = new Run() { Text = $"@{comment.Parent.UserName}", Foreground = App.Current.Resources["ThemeColorBrush"] as SolidColorBrush };
                    parentTextBlock.Inlines.Add(link);

                    parentTextBlock.Inlines.Add(new Run() { Text = $": {comment.Content}" });
                }
                else
                {
                    parentTextBlock.Inlines.Add(new Run() { Text = comment.Content });
                }
            }
        }

        private void Grid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            menuFlyout.ShowAt(element, e.GetPosition(element));
        }

        private void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            //FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(element);
            //FlyoutBase.ShowAttachedFlyout(element);

            menuFlyout.ShowAt(element, e.GetPosition(element));
        }

        private void UserControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var comment = (Comment)args.NewValue;
            if (comment != null)
            {
                if (comment.Parent != null)
                {
                    parentTextBlock.Inlines.Add(new Run() { Text = "回复" });

                    Run link = new Run() { Text = $"@{comment.Parent.UserName}", Foreground = App.Current.Resources["ThemeColorBrush"] as SolidColorBrush };
                    parentTextBlock.Inlines.Add(link);

                    parentTextBlock.Inlines.Add(new Run() { Text = $": {comment.Content}" });
                }
                else
                {
                    parentTextBlock.Inlines.Add(new Run() { Text = comment.Content });
                }
            }
        }
    }
}
