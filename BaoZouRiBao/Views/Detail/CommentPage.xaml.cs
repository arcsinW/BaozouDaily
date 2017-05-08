using BaoZouRiBao.Controls;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Model;
using BaoZouRiBao.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{
    public sealed partial class CommentPage : Page
    {
        public CommentPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string documentId = (string)e.Parameter;
            if(!string.IsNullOrEmpty(documentId))
            {
                ViewModel.SetDocumentId(documentId);               
            }
        }
         
        private bool isHotLoaded = false;
        private bool isLatestLoaded = false;

        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    if(!isLatestLoaded)
                    {
                        ViewModel.GetLatestComments();
                        isLatestLoaded = true;
                    }
                    break;
                case 1:
                    if(!isHotLoaded)
                    {
                        ViewModel.GetHotComments();
                        isHotLoaded = true;
                    }
                    break;
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    ViewModel.RefreshLatestComments();
                    break;
                case 1:
                    ViewModel.RefreshHotComments();
                    break;
            }
        }

        protected override async void OnKeyDown(KeyRoutedEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (!string.IsNullOrEmpty(ViewModel.Content))
                {
                    if (isReplyComment)
                    {
                        await ViewModel.ReplyCommentAsync(currentParentId);
                    }
                    else
                    {
                        await ViewModel.CommentAsync();
                    }
                }
            }
        }

        private void replyBtn_Click(object sender, RoutedEventArgs e)
        {
            Comment comment = ((Button)e.OriginalSource).DataContext as Comment;
            if (comment != null)
            {
                commentTextBox.PlaceholderText = $"@{comment.User.Name}";
                commentTextBox.Focus(FocusState.Programmatic);

                currentParentId = comment.Id;

                isReplyComment = true;
            }
        }

        private async void Comment(object sender, RoutedEventArgs e)
        {
            if (isReplyComment)
            {
                var result = await ViewModel.ReplyCommentAsync(currentParentId);

                if (result != null && string.IsNullOrEmpty(result.Id))
                {
                    ToastService.SendToast("评论成功");
                }
            }
            else
            {
                var result = await ViewModel.CommentAsync();
                if (result != null && !string.IsNullOrEmpty(result.Id))
                {
                    ToastService.SendToast("评论成功");
                }
            }
        }

        /// <summary>
        /// 是否是回复评论
        /// </summary>
        private bool isReplyComment = false;

        private string currentParentId = string.Empty;

        private void commentTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            commentTextBox.PlaceholderText = "忍不住吐个槽";
            isReplyComment = false;
        }
    }
}
