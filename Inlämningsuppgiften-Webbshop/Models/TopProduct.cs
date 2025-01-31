using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop.Models
{
    internal class TopProduct
    {
        public int Id { get; set; }  
        public int ProductId { get; set; }  
        public int Ranking { get; set; }  

        public virtual Product Product { get; set; }  
    }
}
