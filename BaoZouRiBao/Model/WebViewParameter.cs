using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoZouRiBao.Model
{
    /// <summary>
    /// Paramter transfer  to WebViewPage
    /// </summary>
    public class WebViewParameter
    {
        public string Title { get; set; }

        public string WebViewUri { get; set; }

        public object Data { get; set; }

        public string DocumentId { get; set; }

        public string DisplayType { get; set; }
    }
}
