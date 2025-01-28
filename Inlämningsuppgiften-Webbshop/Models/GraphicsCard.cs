using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop.Models
{
    internal class GraphicsCard : Product
    {
        public string Brand { get; set; } = string.Empty;
        public string Vram { get; set; } = string.Empty;
        public string PowerConsumption { get; set; } = string.Empty;
        public string RecommendedPSU { get; set; } = string.Empty;

    }
}
