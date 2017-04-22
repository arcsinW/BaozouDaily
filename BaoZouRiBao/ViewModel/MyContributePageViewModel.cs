using BaoZouRiBao.Http;
using BaoZouRiBao.IncrementalCollection;
using BaoZouRiBao.Model;
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
            Contributes = new IncrementalLoadingList<Contribute>(GetContributes, () => { IsActive = false; }, () => { IsActive = true; });

            if (IsDesignMode)
            {

            }
        }
         
        #region Properties
        public IncrementalLoadingList<Contribute> Contributes { get; set; }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        }

        private bool isEmpty;

        public bool IsEmpty
        {
            get { return isEmpty; }
            set { isEmpty = value; OnPropertyChanged(); }
        }

        #endregion

        public void LoadDesignData()
        {
            string json = "";
        }

        public async Task<IEnumerable<Contribute>> GetContributes(uint count, string timeStamp)
        {
            List<Contribute> contributes = new List<Contribute>();

            if (timeStamp.Equals("0"))
            {
                Contributes.NoMore();

                return contributes;
            }
            var result = await ApiService.Instance.GetMyContributeAsync(timeStamp);
            if (result != null && result != null)
            {
                Contributes.TimeStamp = result.TimeStamp;

                foreach (var item in result.Contributes)
                {
                    contributes.Add(item);
                }
            } 

            if (contributes.Count == 0 && Contributes.Count == 0)
            {
                IsEmpty = true;
            }

            return contributes;
        }
    }
}
