using Inlämningsuppgiften_Webbshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop
{
    internal class ShopExample
    {
        public static void DrawShop()
        {
            using (var myDb = new ShopDbContext())
            {
                
                var topProducts = myDb.TopProducts
                    .Join(myDb.Products, tp => tp.ProductId, p => p.ProductId, (tp, p) => new { tp.Ranking, p.Name, p.Description, p.Price })
                    .OrderBy(tp => tp.Ranking)
                    .ToList();

  
                List<string> topText1 = new List<string> { "# LvlUpGames #", "Allt inom gaming och hårdvara" };
                var windowTop1 = new Window("", 2, 1, topText1);
                windowTop1.Draw();

                
                if (topProducts.Count >= 1)
                {
                    var product1 = topProducts.FirstOrDefault(tp => tp.Ranking == 1);
                    List<string> topText2 = new List<string>
                    {
                        product1?.Name ?? "Ingen produkt",
                        product1?.Description ?? "Ingen beskrivning",
                        $"Pris: {product1?.Price ?? 0} kr",
                        "Tryck 6 för att köpa"
                    };
                    var windowTop2 = new Window("Erbjudande 1", 2, 6, topText2);
                    windowTop2.Draw();
                }

                if (topProducts.Count >= 2)
                {
                    var product2 = topProducts.FirstOrDefault(tp => tp.Ranking == 2);
                    List<string> topText3 = new List<string>
                    {
                        product2?.Name ?? "Ingen produkt",
                        product2?.Description ?? "Ingen beskrivning",
                        $"Pris: {product2?.Price ?? 0} kr",
                        "Tryck 7 för att köpa"
                    };
                    var windowTop3 = new Window("Erbjudande 2", 54, 6, topText3);
                    windowTop3.Draw();
                }

                if (topProducts.Count >= 3)
                {
                    var product3 = topProducts.FirstOrDefault(tp => tp.Ranking == 3);
                    List<string> topText4 = new List<string>
                    {
                        product3?.Name ?? "Ingen produkt",
                        product3?.Description ?? "Ingen beskrivning",
                        $"Pris: {product3?.Price ?? 0} kr",
                        "Tryck 8 för att köpa"
                    };
                    var windowTop4 = new Window("Erbjudande 3", 100, 6, topText4);
                    windowTop4.Draw();
                }
            }
        }

        

    }
}
