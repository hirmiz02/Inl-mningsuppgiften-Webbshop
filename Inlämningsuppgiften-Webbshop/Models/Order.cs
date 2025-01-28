using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop.Models
{
    internal class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string DeliveryOption { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }

        // Navigation Properties
        public Customer Customer { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
