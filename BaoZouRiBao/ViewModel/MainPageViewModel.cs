using BaoZouRiBao.Helper;
using BaoZouRiBao.Model;
using BaoZouRiBao.Model.ResultModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaoZouRiBao.IncrementalCollection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using BaoZouRiBao.Views;

namespace BaoZouRiBao.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            Documents = new DocumentCollection();
            Videos = new VideoCollection();
            Contributes = new ContributeCollection();
            User = GlobalValue.Current.User;
            
            #region DataLoad event
            Documents.OnDataLoadedEvent += OnDataLoadedEvent;
            Documents.OnDataLoadingEvent += OnDataLoadingEvent;

            Videos.OnDataLoadedEvent += OnDataLoadedEvent;
            Videos.OnDataLoadingEvent += OnDataLoadingEvent;

            Contributes.OnDataLoadingEvent += OnDataLoadingEvent;
            Contributes.OnDataLoadedEvent += OnDataLoadedEvent;
            #endregion

            GlobalValue.Current.DataChanged += Current_DataChanged;
   
        } 
        private void OnDataLoadedEvent()
        {
            IsActive = false;
        }

        private void OnDataLoadingEvent()
        {
            IsActive = true;
        } 
        
        #region Properties
        public DocumentCollection Documents { get; set; }
  
        public VideoCollection Videos { get; set; }

        public ContributeCollection Contributes { get; set; }

        private User user = null;
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged();
            }
        }

        public Document SelectedItem { get; set; }
        #endregion

        private void Current_DataChanged()
        {
            this.User = GlobalValue.Current.User;
        }
         
        public void RefreshDocument()
        {
            IsActive = true;           
            Documents.Clear();
            Documents.TopStories.Clear();
            Documents.Reset();
            IsActive = false;
        }

        public void RefreshContribute()
        {
            IsActive = true;
            Contributes.Clear();
            Contributes.Reset();
            IsActive = false;
        }

        public void RefreshVideo()
        {
            IsActive = true;
            Videos.Clear();
            Videos.Reset();
            IsActive = false;
        }


        public void documentListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var story = e.ClickedItem as Document;
            if (story != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = "", WebViewUri = story.Url, DocumentId = story.DocumentId, DisplayType = story.DisplayType };
                NavigationHelper.DetailFrameNavigate(typeof(WebViewPage), parameter);
            }
        }
         
        public void videoListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var video = e.ClickedItem as Video;
            NavigationHelper.DetailFrameNavigate(typeof(VideoPage), video);
        }

        public void contributeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var contribute = e.ClickedItem as Contribute;
            if (contribute != null)
            {
                WebViewParameter parameter = new WebViewParameter() { Title = contribute.Title, DocumentId = contribute.DocumentId, DisplayType = contribute.DisplayType, WebViewUri = contribute.Url };
                NavigationHelper.DetailFrameNavigate(typeof(WebViewPage), parameter);
            }
        }
    }
}
