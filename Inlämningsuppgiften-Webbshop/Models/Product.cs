using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop.Models
{
    internal class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int Stock { get; set; } // Stock level
        public string Supplier { get; set; } = string.Empty; // Supplier name

        // Navigation Property
        public virtual ICollection<TopProduct> TopProducts { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
