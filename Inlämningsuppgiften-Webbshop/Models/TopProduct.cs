using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop.Models
{
    internal class TopProduct
    {
        public int Id { get; set; }  // Unikt ID för varje topprodukt  
        public int ProductId { get; set; }  // Referens till produktens ID  
        public int Ranking { get; set; }  // Ranking för topprodukten

        public virtual Product Product { get; set; }  // Navigation property  
    }
}
