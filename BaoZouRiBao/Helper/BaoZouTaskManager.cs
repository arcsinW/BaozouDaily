using BaoZouRiBao.Enums;
using BaoZouRiBao.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Helper
{
    public class BaoZouTaskManager
    {
        public static async void DailySign()
        {
            var result = await ApiService.Instance.TaskDone(BaoZouTaskEnum.DailySign.ToString());

        }

        public static async void ReadDocument()
        {
            var result = await ApiService.Instance.TaskDone(BaoZouTaskEnum.ReadDocument.ToString());
        }


    }
}
