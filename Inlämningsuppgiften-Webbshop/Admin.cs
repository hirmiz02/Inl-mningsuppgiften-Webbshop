using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using Inlämningsuppgiften_Webbshop.Models;
using System.Collections;

namespace Inlämningsuppgiften_Webbshop
{
    enum Variable
    {
        Int = 0,
        varchar,
        nvarchar,
        Decimal,
        Float,
        Date,
        Time,
        Datetime,
        Bit
    }

    internal class Admin
    {
        public static void WelcomeText()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Välkommen till ADMIN sidan.");
                List<string> topText1 = new List<string> { "1. Administrera produkter", "2. Administrera kategorier", "3. Administrera kunder", "4. Se statistik(Queries)", "5. Avsluta admin läge" };
                var windowTop4 = new Window("Admin", 1, 2, topText1);
                windowTop4.Draw();
                Console.Write("Vänligen välj ett alternativ: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Ogiltig inmatning. Vänligen ange en siffra.");
                    Thread.Sleep(2500);
                    continue; 
                }
                switch (choice)
                {
                    case 1:
                        AdministrateProducts();
                        break;
                    case 2:
                        AdministrateCategories();
                        break;
                    case 3:
                        AdministrateCustomers();
                        break;
                    case 4:
                        ShowStatistics();
                        break;
                    case 5:
                        ExitAdmin();
                        break;
                    default:
                        Console.WriteLine("Vänligen ange en siffra mellan 1-5.");
                        Thread.Sleep(2500);
                        break;

                }
            }  
        }
        public static void AdministrateProducts()
        {
            int choice;
            do
            {
                Console.Clear();
                List<string> topText1 = new List<string> { "1. Skapa en produkt", "2. Radera en produkt", "3. Uppdatera en produkt" };
                var windowTop4 = new Window("Admin", 0, 0, topText1);
                windowTop4.Draw();
                Console.Write("Alternativ: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Ogiltig inmatning. Vänligen ange en siffra.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        CreateProduct();
                        break;
                    case 2:
                        DeleteProduct();
                        break;
                    case 3:
                        UpdateProduct();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("====================================");
                        Console.WriteLine("Vänligen ange en siffra mellan 1-3!");
                        Console.WriteLine("====================================");
                        Thread.Sleep(3500);
                        break;
                }

            } while (choice < 1 || choice > 3);
        }


        public static void AdministrateCategories()
        {
            // OFULLSTÄNDIG, DETTA ÄR FÖR ATT LÄGGA TILL KOLUMNER TILL NYA KATEGORIER
            string columnName;
            string variable;
            string answer;
            do
            {
                Console.WriteLine("Vill du lägga till unik kolumn till tabellen(ja / nej)");
                answer = Console.ReadLine();
                if (answer.ToLower() == "ja")
                {
                    Console.Write("Ange kolumnnamn: ");
                    columnName = Console.ReadLine();
                    Console.WriteLine("Lista på variabler: ");
                    foreach (var i in Enum.GetValues(typeof(Variable)))
                    {
                        Console.WriteLine(Enum.GetName(typeof(Variable), i));
                    }
                    // HUR KAN JAG GÖRA SÅ ATT INPUT FRÅN ANVÄNDARE MATCHAR KONSTANTERNA I ENUMET "Variable"?
                    Console.Write("Ange variabel: ");
                    variable = Console.ReadLine();
                }
            } while (answer.ToLower() == "ja");
        }
        public static void AdministrateCustomers()
        {
            

        }
        public static void ShowStatistics()
        {
            //detta ska göras med DAPPER efter jag infört massa testdata
            Console.WriteLine("test");
            Console.ReadKey(true);
        }

        public static void ExitAdmin()
        {
            Console.WriteLine("test");
            Console.ReadKey(true);
        }

        public static void CreateProduct()
        {
            using (var myDb = new Models.ShopDbContext())
            {
                
                foreach (var item in myDb.Products)
                {
                    Console.WriteLine(item.ProductId + "\t" + item.Name + "\t" + item.Price);
                }
                Console.WriteLine();
                foreach(var item in myDb.Categories)
                {
                    Console.WriteLine("KategoriId: " + item.CategoryId + "\t" + item.Name);
                }

                Console.WriteLine();
                
                Console.Write("Ange produktnamn: ");
                var productName = Console.ReadLine();

                Console.Write("Ange pris: ");
                decimal productPrice;
                while (!decimal.TryParse(Console.ReadLine(), out productPrice) || productPrice <= 0)
                {
                    Console.Write("Ogiltigt pris. Försök igen: ");
                }

                Console.Write("Ange produktbeskrivning: ");
                var productDescription = Console.ReadLine();

                Console.Write("Ange Kategori-ID: ");
                int categoryId;
                while (!int.TryParse(Console.ReadLine(), out categoryId) || categoryId <= 0)
                {
                    Console.Write("Ogiltigt kategori-ID. Försök igen: ");
                }

                Console.Write("Ange antal i lager: ");
                int productStock;
                while (!int.TryParse(Console.ReadLine(), out productStock) || productStock < 0)
                {
                    Console.Write("Ogiltigt antal. Försök igen: ");
                }

                Console.Write("Ange leverantör: ");
                var productSupplier = Console.ReadLine();

                
                object newProduct = null;

                switch (categoryId)
                {
                    case 1: // Game
                        Console.Write("Ange genre: ");
                        var gameGenre = Console.ReadLine();

                        Console.Write("Ange utgivare: ");
                        var gamePublisher = Console.ReadLine();

                        Console.Write("Ange plattform: ");
                        var gamePlatform = Console.ReadLine();

                        newProduct = new Game
                        {
                            Name = productName,
                            Price = productPrice,
                            Description = productDescription,
                            Stock = productStock,
                            Supplier = productSupplier,
                            Genre = gameGenre,
                            Publisher = gamePublisher,
                            Platform = gamePlatform,
                            CategoryId = categoryId
                        };
                        break;

                    case 2: // GraphicsCard
                        Console.Write("Ange Märke: ");
                        var gpuBrand = Console.ReadLine();

                        Console.Write("Ange mängd VRAM: ");
                        var gpuVram = Console.ReadLine();

                        Console.Write("Ange strömförbrukning: ");
                        var gpuPowerConsumption = Console.ReadLine();

                        Console.Write("Ange rekommenderad nätaggregat: ");
                        var gpuRecommendedPSU = Console.ReadLine();

                        newProduct = new GraphicsCard
                        {
                            Name = productName,
                            Price = productPrice,
                            Description = productDescription,
                            Stock = productStock,
                            Supplier = productSupplier,
                            Brand = gpuBrand,
                            Vram = gpuVram,
                            PowerConsumption = gpuPowerConsumption,
                            RecommendedPSU = gpuRecommendedPSU,
                            CategoryId = categoryId
                        };
                        break;

                    case 3: // Monitor
                        Console.Write("Ange Märke: ");
                        var monitorBrand = Console.ReadLine();

                        Console.Write("Ange skärmens uppdateringsfrekvens: ");
                        var monitorRefreshRate = Console.ReadLine();

                        Console.Write("Ange skärmens upplösning: ");
                        var monitorResolution = Console.ReadLine();

                        Console.Write("Ange skärmens paneltyp: ");
                        var monitorPanelType = Console.ReadLine();

                        Console.Write("Ange skärmstorlek: ");
                        var monitorSize = Console.ReadLine();

                        newProduct = new Models.Monitor
                        {
                            Name = productName,
                            Price = productPrice,
                            Description = productDescription,
                            Stock = productStock,
                            Supplier = productSupplier,
                            Brand = monitorBrand,
                            RefreshRate = monitorRefreshRate,
                            Resolution = monitorResolution,
                            PanelType = monitorPanelType,
                            Size = monitorSize,
                            CategoryId = categoryId
                        };
                        break;

                    case 4: // Merch
                        Console.Write("Ange storlek: ");
                        var merchSize = Console.ReadLine();

                        Console.Write("Ange material: ");
                        var merchMaterial = Console.ReadLine();

                        newProduct = new Merch
                        {
                            Name = productName,
                            Price = productPrice,
                            Description = productDescription,
                            Stock = productStock,
                            Supplier = productSupplier,
                            Size = merchSize,
                            Material = merchMaterial,
                            CategoryId = categoryId
                        };
                        break;

                    default:
                        Console.WriteLine("Kategori med detta ID existerar inte.");
                        return;
                }

                
                if (newProduct != null)
                {
                    myDb.Products.Add((Product)newProduct);
                    myDb.SaveChanges();
                    Console.WriteLine($"Produkt '{productName}' har lagts till i databasen.");
                }
            }
        }

        public static void DeleteProduct()
        {
            Console.WriteLine("test");
        }
        public static void UpdateProduct()
        {
            Console.WriteLine("test");
            Console.ReadKey(true);
        }
    }
}
