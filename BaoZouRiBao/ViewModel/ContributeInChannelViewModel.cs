using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.ViewModel
{
    public class ContributeInChannelViewModel : ViewModelBase
    {
        public ContributeInChannelCollection Contributes { get; set; }

        private bool isActive ;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        }


        public ContributeInChannelViewModel()
        {
            if(IsDesignMode)
            {
                Initial("3");
            }
        }

        public void Initial(string id)
        {
            Contributes = new ContributeInChannelCollection(id);
        }
    }
}
