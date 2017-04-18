using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class MyContributePageViewModel : ViewModelBase
    {
        public MyContributePageViewModel()
        {

        }

        #region Properties
        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        } 
        #endregion

    }
}
