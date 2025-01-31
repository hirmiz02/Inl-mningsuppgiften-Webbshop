using Inlämningsuppgiften_Webbshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop
{
    enum ShippingMethod
    {
        Hämta_Hos_Ombud_29kr,
        Hemleverans_99kr
    }

    enum PaymentMethod
    {
        Banköverföring,
        Swish,
        Klarna
    }
    
    internal class CustomerUI
    {
        public static List<Product> shoppingCart = new List<Product>();
        public static void ShowCategories()
        {
            using (var myDb = new ShopDbContext())
            {
                int i = 0;
                foreach (var category in myDb.Categories)
                {
                    i++;
                    Console.WriteLine(i + "\t" + category.Name);
                }


                Console.WriteLine("Tryck på en knapp (1-4) för att välja motsvarande kategori:");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.KeyChar)
                {
                    case '1':
                        Console.WriteLine("\nLista av alla spel: ");
                        Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                        int index = 1;
                        foreach (var game in myDb.Games)
                        {
                            Console.WriteLine(
                                $"{index++}. {game.Name.PadRight(30)}" +
                                $"{game.Price.ToString("F2").PadRight(10)}" +
                                $"{game.Genre.PadRight(20)}"
                            );
                        }


                        Game selectedGame = null;
                        bool loop = true;
                        while (loop)
                        {
                            Console.Write("\nVänligen ange numret för att välja ett spel: ");
                            string userInput = Console.ReadLine();
                            if (int.TryParse(userInput, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= myDb.Games.Count())
                            {
                                selectedGame = myDb.Games.ElementAt(selectedIndex - 1);
                                Console.Clear();
                                Console.WriteLine(
                                    "Namn: " + selectedGame.Name.PadRight(30) + "\n" +
                                    "Pris: " + selectedGame.Price.ToString("F2").PadRight(10) + "\n" +
                                    "Genre: " + selectedGame.Genre.PadRight(20) + "\n" +
                                    "Beskrvining: " + selectedGame.Description.PadRight(70) + "\n" +
                                    "Plattform: " + selectedGame.Platform.PadRight(15) + "\n" +
                                    "Lager: " + selectedGame.Stock.ToString().PadRight(5)
                                );
                                loop = false;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt val, försök igen.");
                                Thread.Sleep(2000);
                            }
                        }
                        Console.WriteLine("\nKlicka B för att köpa denna produkt.");
                        ConsoleKeyInfo buyButton = Console.ReadKey(true);
                        if (buyButton.KeyChar == 'B' || buyButton.KeyChar == 'b')
                        {
                            shoppingCart.Add(selectedGame);
                        }
                        break;


                    case '2':
                        Console.WriteLine("\nLista av alla grafikkort: ");
                        Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                        int iterate = 1;
                        foreach (var graphicsCard in myDb.GraphicsCards)
                        {
                            Console.WriteLine(
                                $"{iterate++}. {graphicsCard.Name.PadRight(30)}" +
                                $"{graphicsCard.Price.ToString("F2")}"
                            );
                        }

                        GraphicsCard selectedGPU = null;
                        bool loop2 = true;
                        while (loop2)
                        {
                            Console.Write("\nVänligen ange numret för att välja ett grafikkort: ");
                            string userInput = Console.ReadLine();
                            if (int.TryParse(userInput, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= myDb.GraphicsCards.Count())
                            {
                                selectedGPU = myDb.GraphicsCards.ElementAt(selectedIndex - 1);
                                Console.Clear();
                                Console.WriteLine(
                                    "Namn: " + selectedGPU.Name.PadRight(30) + "\n" +
                                    "Pris: " + selectedGPU.Price.ToString("F2").PadRight(10) + "\n" +
                                    "Märke: " + selectedGPU.Brand.PadRight(20) + "\n" +
                                    "Beskrvining: " + selectedGPU.Description.PadRight(70) + "\n" +
                                    "VRAM: " + selectedGPU.Vram.PadRight(15) + "\n" +
                                    "Strömförbrukning: " + selectedGPU.PowerConsumption.PadRight(15) + "\n" +
                                    "Rekommenderad nätaggreggat: " + selectedGPU.RecommendedPSU.PadRight(15) + "\n" +
                                    "Lager: " + selectedGPU.Stock.ToString().PadRight(5)
                                );
                                loop2 = false;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt val, försök igen.");
                                Thread.Sleep(2000);
                            }
                        }
                        Console.WriteLine("\nKlicka B för att köpa denna produkt.");
                        ConsoleKeyInfo buyGPU = Console.ReadKey(true);
                        if (buyGPU.KeyChar == 'B' || buyGPU.KeyChar == 'b')
                        {
                            shoppingCart.Add(selectedGPU);
                        }
                        break;
                    case '3':
                        Console.WriteLine("\nLista av alla skärmar: ");
                        Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                        int index2 = 1;
                        foreach (var monitor in myDb.Monitors)
                        {
                            Console.WriteLine(
                                $"{index2++}. {monitor.Name.PadRight(30)}" +
                                $"{monitor.Price.ToString("F2").PadRight(10)}" +
                                $"{monitor.Resolution.PadRight(20)}"
                            );
                        }


                        Models.Monitor selectedMonitor = null;
                        bool loop3 = true;
                        while (loop3)
                        {
                            Console.Write("\nVänligen ange numret för att välja en skärm: ");
                            string userInput = Console.ReadLine();
                            if (int.TryParse(userInput, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= myDb.Monitors.Count())
                            {
                                selectedMonitor = myDb.Monitors.ElementAt(selectedIndex - 1);
                                Console.Clear();
                                Console.WriteLine(
                                    "Namn: " + selectedMonitor.Name.PadRight(30) + "\n" +
                                    "Pris: " + selectedMonitor.Price.ToString("F2").PadRight(10) + "\n" +
                                    "Märke: " + selectedMonitor.Brand.PadRight(20) + "\n" +
                                    "Beskrvining: " + selectedMonitor.Description.PadRight(70) + "\n" +
                                    "Uppdateringsfrekvens: " + selectedMonitor.RefreshRate.PadRight(15) + "\n" +
                                    "Upplösning: " + selectedMonitor.Resolution.PadRight(15) + "\n" +
                                    "Paneltyp: " + selectedMonitor.PanelType.PadRight(15) + "\n" +
                                    "Skärmstorlek: " + selectedMonitor.Size.PadRight(15) + "\n" +
                                    "Lager: " + selectedMonitor.Stock.ToString().PadRight(5)
                                );
                                loop3 = false;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt val, försök igen.");
                                Thread.Sleep(2000);
                            }
                        }
                        Console.WriteLine("\nKlicka B för att köpa denna produkt.");
                        ConsoleKeyInfo buyMonitor = Console.ReadKey(true);
                        if (buyMonitor.KeyChar == 'B' || buyMonitor.KeyChar == 'b')
                        {
                            shoppingCart.Add(selectedMonitor);
                        }
                        break;
                    case '4':
                        Console.WriteLine("\nLista av all merch: ");
                        Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                        int iterate2 = 1;
                        foreach (var merch in myDb.Merches)
                        {
                            Console.WriteLine(
                                $"{iterate2++}. {merch.Name.PadRight(30)}" +
                                $"{merch.Price.ToString("F2")}"
                            );
                        }

                        Merch selectedMerch = null;
                        bool loop4 = true;
                        while (loop4)
                        {
                            Console.Write("\nVänligen ange numret för att välja merch: ");
                            string userInput = Console.ReadLine();
                            if (int.TryParse(userInput, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= myDb.Merches.Count())
                            {
                                selectedMerch = myDb.Merches.ElementAt(selectedIndex - 1);
                                Console.Clear();
                                Console.WriteLine(
                                    "Namn: " + selectedMerch.Name.PadRight(30) + "\n" +
                                    "Pris: " + selectedMerch.Price.ToString("F2").PadRight(10) + "\n" +
                                    "Beskrvining: " + selectedMerch.Description.PadRight(70) + "\n" +
                                    "Storlek: " + selectedMerch.Size.PadRight(15) + "\n" +
                                    "Material: " + selectedMerch.Material.PadRight(15) + "\n"
                                );
                                loop4 = false;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt val, försök igen.");
                                Thread.Sleep(2000);
                            }
                        }
                        Console.WriteLine("\nKlicka B för att köpa denna produkt.");
                        ConsoleKeyInfo buyMerch = Console.ReadKey(true);
                        if (buyMerch.KeyChar == 'B' || buyMerch.KeyChar == 'b')
                        {
                            shoppingCart.Add(selectedMerch);
                        }
                        break;
                }
            }
        }

        public static void ShowShoppingCart()
        {
            decimal productsSum = 0;

            Console.Clear();
            Console.WriteLine("==============");
            Console.WriteLine("Din varukorg:");
            Console.WriteLine("==============\n");

            foreach (var item in shoppingCart)
            {
                Console.WriteLine(item.ProductId + "\t" + item.Name + "\t" + item.Price + "kr");
                productsSum += item.Price;
            }
            Console.WriteLine("\nTotal summa: " + productsSum);

            List<string> topText3 = new List<string> { "1. Ändra antal av en produkt", "2. Ta bort en produkt", "3. Gå vidare till frakt-vy ->" };
            var windowTop3 = new Window("Kundmeny", 80, 15, topText3);
            windowTop3.Draw();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.KeyChar)
            {

                case '1':
                    Console.Write("Ange produktens ID: ");
                    string userInput = Console.ReadLine();

                    if (int.TryParse(userInput, out int productToUpdateId))
                    {
                        var item = shoppingCart.FirstOrDefault(p => p.ProductId == productToUpdateId);
                        if (item != null)
                        {
                            shoppingCart.Add(item);
                            Console.WriteLine("Ökade antal av produkt: " + item.Name + " med 1");
                            Thread.Sleep(2800);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Kunde ej hitta produkt med detta ID i din varukorg.");
                        Thread.Sleep(2000);
                    }
                    break;
                case '2':
                    Console.Write("Ange produktens ID: ");
                    string deleteProductId = Console.ReadLine();

                    if (int.TryParse(deleteProductId, out int productToDeleteId))
                    {
                        var item = shoppingCart.FirstOrDefault(p => p.ProductId == productToDeleteId);
                        if (item != null)
                        {
                            shoppingCart.Remove(item);
                            Console.WriteLine("Produkten: " + item.Name + " är borttagen.");
                            Thread.Sleep(2800);
                        }
                    }
                    break;
                case '3':
                    if (!shoppingCart.Any())
                    {
                        Console.WriteLine("Din varukorg är tom. Lägg till produkter för att fortsätta med köpet.");
                        Console.ReadKey(true);
                        return;
                    }
                    Shipping();
                    break;
            }
        }

        public static void Shipping()
        {
            using (var myDb = new ShopDbContext())
            {                             
                Console.Clear();

                Console.Write("Ange fullständigt namn: ");
                string fullName = Console.ReadLine();

                Console.Write("Ange gata: ");
                string street = Console.ReadLine();

                Console.Write("Ange stad: ");
                string city = Console.ReadLine();

                Console.Write("Ange land: ");
                string country = Console.ReadLine();

                Console.Write("Ange mobilnummer: ");
                string phoneNumber = Console.ReadLine();

                Console.Write("Ange e-mail: ");
                string email = Console.ReadLine();

                Console.Write("Ange ålder: ");
                if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
                {
                    Console.WriteLine("Ogiltig ålder. Försök igen.");
                    return;
                }

                var newCustomer = new Customer
                {
                    Name = fullName,
                    Street = street,
                    City = city,
                    Country = country,
                    Phone = phoneNumber,
                    Email = email,
                    Age = age
                };
                myDb.Customers.Add(newCustomer);
                myDb.SaveChanges();

                var currentOrder = CreateOrder(newCustomer.CustomerId);

                Console.WriteLine("\nVälj leveranssätt:");
                foreach (int i in Enum.GetValues(typeof(ShippingMethod)))
                {
                    Console.WriteLine($"{i + 1}. {Enum.GetName(typeof(ShippingMethod), i).Replace('_', ' ')}");
                }

                Console.Write("\nAnge ditt val: ");
                if (!int.TryParse(Console.ReadLine(), out int input) || input < 1 || input > Enum.GetValues(typeof(ShippingMethod)).Length)
                {
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    return;
                }

                int shippingCost = 0;
                switch (input)
                {
                    case 1:
                        Console.WriteLine("\nDu har valt: HÄMTA HOS OMBUD\n");
                        shippingCost = 29;
                        currentOrder.DeliveryOption = "Hämta hos Ombud";
                        break;
                    case 2:
                        Console.WriteLine("\nDu har valt: HEMLEVERANS\n");
                        shippingCost = 99;
                        currentOrder.DeliveryOption = "Hemleverans";
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val.");
                        return;
                }

                Console.WriteLine("\n==================================================");
                Console.WriteLine("\nKlicka ENTER för att gå vidare till betalvyn.");
                Console.ReadKey(true);

                ViewPayment(shippingCost, currentOrder, newCustomer);
            }
        }

        public static void ViewPayment(int shippingCost, Order currentOrder, Customer newCustomer)
        {
            Console.Clear();
            using (var myDb = new ShopDbContext())
            {
                decimal productSum = 0;
                Console.WriteLine("Din varukorg: ");
                Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯");
                foreach (var item in shoppingCart)
                {
                    Console.WriteLine(item.ProductId + "\t" + item.Name + "\t" + item.Price);
                    productSum += item.Price;
                }
                Console.WriteLine("\nTotal summa inklusive frakt och moms : " + (productSum + shippingCost) + "kr");


                Console.WriteLine("\n Välj betalmetod (1-3)");
                foreach (int i in Enum.GetValues(typeof(PaymentMethod)))
                {
                    Console.WriteLine($"{i + 1}. {Enum.GetName(typeof(PaymentMethod), i).Replace('_', ' ')}");
                }

                Console.Write("\nAnge ditt val: ");
                if (!int.TryParse(Console.ReadLine(), out int input) || input < 1 || input > Enum.GetValues(typeof(PaymentMethod)).Length)
                {
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    return;
                }

                switch (input)
                {
                    case 1:
                        Console.WriteLine("\nDu har valt: BANKÖVERFÖRING\n");
                        currentOrder.PaymentMethod = "Banköverföring";
                        break;
                    case 2:
                        Console.WriteLine("\nDu har valt: SWISH\n");
                        currentOrder.PaymentMethod = "Swish";
                        break;
                    case 3:
                        Console.WriteLine("\nDu har valt: KLARNA");
                        currentOrder.PaymentMethod = "Klarna";
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val.");
                        return;
                }
                Console.WriteLine("\nKlicka ENTER för att genomföra köp.");
                Console.ReadKey(true);
                currentOrder.TotalAmount = (productSum + shippingCost);
                currentOrder.CustomerId = newCustomer.CustomerId;
                currentOrder.OrderDate = DateTime.Now;

                myDb.Orders.Update(currentOrder);
                myDb.SaveChanges();

                Console.Clear();
                Console.WriteLine("Tack för ditt köp! Ditt ordernummer: " + currentOrder.OrderId + "\n");
                
                Console.WriteLine("\nDina köpta produkter:");
                foreach(var item in shoppingCart)
                {
                    Console.WriteLine(item.Name + "\t" + item.Price);
                }
                
                Console.WriteLine("\n===========================");
                Console.WriteLine("\nTotala summa: " + (productSum + shippingCost) + "kr");
                Console.ReadKey(true);

                shoppingCart.Clear();
            }
        }

        public static void SearchForProduct(int Id)
        {
            using (var myDb = new ShopDbContext())
            {
                string productName = " ";
                if (Id == 0)
                {
                    Console.Write("Ange namnet på produkten du söker efter: ");
                    productName = Console.ReadLine();
                }
                

                foreach (var item in myDb.Products)
                {
                    if (item.Name.ToLower() == productName.ToLower() || item.ProductId == Id)
                    {
                        Console.WriteLine(
                            "Namn: " + item.Name.PadRight(30) + "\n" +
                            "Pris: " + item.Price.ToString("F2").PadRight(10) + "\n" +
                            "Beskrivning: " + item.Description.PadRight(70) + "\n" +
                            "Lager: " + item.Stock.ToString().PadRight(5)
                        );

                        if (item is Game game)
                        {
                            Console.WriteLine(
                                "Genre: " + game.Genre.PadRight(20) + "\n" +
                                "Plattform: " + game.Platform.PadRight(15)
                            );
                        }
                        if (item is Merch merch)
                        {
                            Console.WriteLine(
                                "Storlek: " + merch.Size.PadRight(15) + "\n" +
                                "Material: " + merch.Material.PadRight(15) + "\n"
                            );
                        }
                        if (item is Models.Monitor monitor)
                        {
                            Console.WriteLine(
                                "Uppdateringsfrekvens: " + monitor.RefreshRate.PadRight(15) + "\n" +
                                "Upplösning: " + monitor.Resolution.PadRight(15) + "\n" +
                                "Paneltyp: " + monitor.PanelType.PadRight(15) + "\n" +
                                "Skärmstorlek: " + monitor.Size.PadRight(15) + "\n" +
                                "Lager: " + monitor.Stock.ToString().PadRight(5)
                            );
                        }
                        if (item is GraphicsCard graphicsCard)
                        {
                            Console.WriteLine(
                                    "Märke: " + graphicsCard.Brand.PadRight(20) + "\n" +
                                    "VRAM: " + graphicsCard.Vram.PadRight(15) + "\n" +
                                    "Strömförbrukning: " + graphicsCard.PowerConsumption.PadRight(15) + "\n" +
                                    "Rekommenderad nätaggreggat: " + graphicsCard.RecommendedPSU.PadRight(15)
                            );
                        }
                        Console.WriteLine("Lägg till produkten till varukorgen (B)");
                        
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if( keyInfo.KeyChar == 'B' || keyInfo.KeyChar == 'b')
                        {
                            shoppingCart.Add(item);
                            Console.WriteLine("Produkten tillagd.");
                            Console.ReadKey(true);
                        }
                    }
                    }
                }
            }
        

        public static Order CreateOrder(int newCustomerId)
        {
            using (var myDb = new ShopDbContext())
            {
                var newOrder = new Order
                {
                    CustomerId = newCustomerId,
                    TotalAmount = 0,
                    OrderDate = DateTime.Now
                };
                
                myDb.Orders.Add(newOrder);
                myDb.SaveChanges();

                return newOrder;
            }
        }

        public static void GetTopProducts(int maxIterations)
        {
            int iterations = 1;
            
            using (var myDb = new ShopDbContext())
            {
                
                var topProducts = myDb.TopProducts
                    .Join(myDb.Products, tp => tp.ProductId, p => p.ProductId, (tp, p) => new
                    {
                        tp.ProductId,  
                        tp.Ranking     
                    })
                    .OrderBy(tp => tp.Ranking)  
                    .ToList();

                
                foreach (var tp in topProducts)
                {                    
                    if(iterations >= maxIterations)
                    {
                        SearchForProduct(tp.ProductId);
                        break;
                    }
                    iterations++;
                }
            }
        }
    }
}
