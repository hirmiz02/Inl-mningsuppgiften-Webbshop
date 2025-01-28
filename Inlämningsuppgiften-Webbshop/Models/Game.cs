using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop.Models
{
    internal class Game : Product
    {
        public string Genre { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;

    }
}
