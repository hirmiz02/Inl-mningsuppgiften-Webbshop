using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop.Models
{
    internal class Monitor : Product
    {
        public string Brand { get; set; } = string.Empty;
        public string RefreshRate { get; set; } = string.Empty;
        public string Resolution { get; set; } = string.Empty;
        public string PanelType { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;

    }
}
