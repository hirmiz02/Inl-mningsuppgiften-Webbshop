using Inlämningsuppgiften_Webbshop;

namespace WindowDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool running = true;  
            while (running)
            {                
                Console.Clear();
                ShopExample.DrawShop();
                List<string> topText3 = new List<string> { "1. Shoppen", "2. Frisökning", "3. Varukorgen", "4. ADMIN", "5. Avsluta" };
                var windowTop3 = new Window("Kundmeny", 5, 15, topText3);
                windowTop3.Draw();

                Console.WriteLine("Vänligen klicka på ett alternativ (1-8):");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.KeyChar)
                {
                    case '1':
                        CustomerUI.ShowCategories();
                        break;

                    case '2':
                        CustomerUI.SearchForProduct(0);
                        break;

                    case '3':
                        CustomerUI.ShowShoppingCart();
                        break;

                    case '4':
                        Admin.WelcomeText();
                        break;

                    case '5':
                        running = false; 
                        break;
                    
                    case '6':
                        Console.Clear();
                        CustomerUI.GetTopProducts(1);
                        break;
                    
                    case '7':
                        Console.Clear();
                        CustomerUI.GetTopProducts(2);
                        break;
                    
                    case '8':
                        Console.Clear();
                        CustomerUI.GetTopProducts(3);
                        break;

                    default:
                        Console.WriteLine("\nOgiltigt val. Tryck på en knapp mellan 1 och 5.");
                        Thread.Sleep(2000);   
                        break;
                }
            }

            Console.WriteLine("Tack för att du besökte! Hejdå!");
        }
    }
}