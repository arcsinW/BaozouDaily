using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Core.Model
{
    public class Settings
    {
        public bool IsAutoCache { get; set; } = false;

        public bool IsSmallImage { get; set; } = false;

        public bool IsBigFont { get; set; } = false;

        public bool IsNewsNotify { get; set; } = false;

        public string CacheFolder { get; set; }

        public string Version { get; set; }
    }
}
