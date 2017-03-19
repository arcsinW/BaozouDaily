using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Helper
{
    public class LogHelper
    {
        public static void WriteLine(Exception e,[CallerMemberName]string name = "")
        {
            Debug.WriteLine(name + e.Message);
        }
    }
}
