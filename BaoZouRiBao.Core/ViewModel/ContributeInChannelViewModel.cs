using BaoZouRiBao.Core.IncrementalCollection;
using BaoZouRiBao.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.ViewModel
{
    public class ContributeInChannelViewModel : ViewModelBase
    {
        public ContributeInChannelCollection Contributes { get; set; }

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
