using BaoZouRiBao.Common;
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
        
        private void StyleParent()
        {
            parentTextBlock.Inlines.Add(new Run());
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
        #endregion
        
    }
}
