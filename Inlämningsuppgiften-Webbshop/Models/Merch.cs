using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop.Models
{
    internal class Merch : Product
    {
        public string Size { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;

    }
}
