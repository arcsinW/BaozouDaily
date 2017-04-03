using System.ComponentModel;
using System.Runtime.CompilerServices;
using BaoZouRiBao.Helper;
using BaoZouRiBao.Views;
using Windows.ApplicationModel;

namespace BaoZouRiBao.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(storage, value))
            {
                storage = value;
                OnPropertyChanged(propertyName);
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        public bool IsDesignMode
        {
            get
            {
                return DesignMode.DesignModeEnabled;
            }
        }

        public bool IsMobile => DeviceInformationHelper.IsMobile();

        // private ElementTheme appTheme = ElementTheme.Light;
        ///// <summary>
        ///// 当前主题
        ///// </summary>
        // public ElementTheme AppTheme
        // {
        //    get { return appTheme; }
        //    set { appTheme = value; OnPropertyChanged(); }
        // }

        public void GoBack() => NavigationHelper.GoBack();

        public void OpenDrawer()
        {
            MasterDetailPage.Current.drawer.IsDrawerOpened = true;
        }

        /// <summary>
        /// 切换日夜间模式
        /// </summary>
        public void SwitchElementTheme()
        {
            GlobalValue.Current.SwitchElementTheme();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public virtual void Refresh() { }
    }
}
