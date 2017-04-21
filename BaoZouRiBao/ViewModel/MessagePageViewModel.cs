using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class MessagePageViewModel : ViewModelBase
    {
        public MessagePageViewModel()
        {
            if(IsDesignMode)
            {
                LoadDesignData();
            }
        }

        private void LoadDesignData()
        {

        }

        #region Properties
        private bool isActive = false;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        } 
        #endregion
    }
}
