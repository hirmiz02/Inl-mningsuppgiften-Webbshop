using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop.Models
{
    internal class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
