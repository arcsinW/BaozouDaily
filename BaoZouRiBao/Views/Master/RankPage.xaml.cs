using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace BaoZouRiBao.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RankPage : Page
    {
        public RankPage()
        {
            this.InitializeComponent();
        }

        private bool isRankTimeChanged = false;

        private bool isReadLoaded = false;
        private bool isVoteLoaded = false;
        private bool isCommentLoaded = false;


        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pivot == null) return;
            isRankTimeChanged = true;
            switch (pivot.SelectedIndex)
            {
                case 0:
                    await ViewModel.RefreshReadCollectionAsync(comboBox.SelectedIndex);
                    break;
                case 1:
                    await ViewModel.RefreshVoteCollectionAsync(comboBox.SelectedIndex);
                    break;
                case 2:
                    await ViewModel.RefreshCommentCollectionAsync(comboBox.SelectedIndex);
                    break;
            }
        }

        private async void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (pivot.SelectedIndex)
            {
                case 0:
                    if (!isReadLoaded || isRankTimeChanged)
                    {
                        Debug.WriteLine($"isReadLoade : {isReadLoaded} {comboBox.SelectedIndex}");
                        await ViewModel.RefreshReadCollectionAsync(comboBox.SelectedIndex);
                        isReadLoaded = true;
                    }
                    break;
                case 1:
                    if(!isVoteLoaded || isRankTimeChanged)
                    {
                        Debug.WriteLine($"isVoteLoaded : {isVoteLoaded} {comboBox.SelectedIndex}");
                        await ViewModel.RefreshVoteCollectionAsync((comboBox.SelectedIndex));
                        isVoteLoaded = true;
                    }
                    break;
                case 2:
                    if(!isCommentLoaded || isRankTimeChanged)
                    {
                        Debug.WriteLine($"isCommentLoaded : {isCommentLoaded} {comboBox.SelectedIndex}");
                        await ViewModel.RefreshCommentCollectionAsync((comboBox.SelectedIndex));
                        isCommentLoaded = true;
                    }
                    break;
            }
        }
    }
}
