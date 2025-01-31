using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using Inlämningsuppgiften_Webbshop.Models;
using System.Collections;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WindowDemo;
using System.Diagnostics;

namespace Inlämningsuppgiften_Webbshop
{
    enum Update
    {
        Lägga_till_kolumn,
        Ta_bort_kolumn,
        Byta_namn_på_kolumn,
        Byta_namn_på_tabell,
        Avsluta
    }

    enum CustomerSettings
    {
        Radera_Kund,
        Uppdatera_Kund,
        Beställningshistorik,
        Avsluta
    }

    enum CustomerInfo
    {
        namn = 1,
        gata,
        stad,
        land,
        nummer,
        mail,
        ålder
    }

    internal class Admin
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog=LvlUpGames; persist security info=True; Integrated Security=True; TrustServerCertificate=True";
        private static List<Product> TopProducts = new List<Product>();

        public static void WelcomeText()
        {           
            while (true)
            {
                var orderCount = AsyncSQL.SQLStatisticsAsync().GetAwaiter().GetResult();
                Console.Clear();
                Console.WriteLine("Välkommen till ADMIN sidan.");
                List<string> topText1 = new List<string> { "1. Administrera produkter", "2. Administrera kategorier", "3. Administrera kunder", "4. Se statistik(Queries)", "5. Uppdatera topp-produkter", "6. Avsluta admin läge" };
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
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        Console.WriteLine("Antal ordrar som gjorts i Eskilstuna: " + orderCount);
                        sw.Stop();
                        Console.WriteLine("Tid för att hämta data asynkront: " + sw.ElapsedMilliseconds + " ms");
                        Console.ReadKey(true);
                        break;
                    case 5:
                        UpdatePopularProducts();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Vänligen ange en siffra mellan 1-6.");
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
                List<string> topText1 = new List<string> { "1. Skapa en produkt", "2. Radera en produkt", "3. Uppdatera en produkt", "4. Gå tillbaka" };
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
                    case 4:
                        return;
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
            int choice;
            do
            {
                Console.Clear();
                List<string> topText1 = new List<string> { "1. Skapa en kategori", "2. Radera en kategori", "3. Uppdatera en kategori", "4. Gå tillbaka" };
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
                        CreateCategory();
                        break;
                    case 2:
                        DeleteCategory();
                        break;
                    case 3:
                        UpdateCategory();
                        break;
                    case 4:
                        return;
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
        
        public static void CreateCategory()
        {
            using(var connection = new SqlConnection(connString))
            {
                connection.Open();

                Console.Write("Ange kategorinamn: ");
                string categoryName = Console.ReadLine();

                string insertCategoryQuery = "INSERT INTO Categories (Name) VALUES (@CategoryName); SELECT SCOPE_IDENTITY();";
                int newCategoryId = connection.ExecuteScalar<int>(insertCategoryQuery, new {CategoryName = categoryName});

                Console.WriteLine($"Ny kategori '{categoryName}' har skapats med ID {newCategoryId}.");

                Console.Write("Ange kolumnnamn för produktkategorin (separera med kommatecken): ");
                string columnNames = Console.ReadLine();

                string[] columns = columnNames.Split(',');
                
                string createTableQuery = BuildCreateTableQuery(categoryName, columns);

                connection.Execute(createTableQuery);

                Console.WriteLine($"Kategorin '{categoryName}' har skapats.");
                Thread.Sleep(5000);
            }

        }
        private static string BuildCreateTableQuery(string categoryName, string[] columns)
        {
            
            string columnDefinitions = "Id INT IDENTITY(1,1) PRIMARY KEY, ";
            foreach (var column in columns)
            {
                columnDefinitions += $"{column.Trim()} NVARCHAR(255), ";
            }
            columnDefinitions = columnDefinitions.TrimEnd(',', ' ');

            
            return $"CREATE TABLE {categoryName} ({columnDefinitions});";
        }

        public static void DeleteCategory()
        {
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = @"
                                SELECT TABLE_NAME
                                FROM INFORMATION_SCHEMA.TABLES
                                WHERE TABLE_TYPE = 'BASE TABLE'
                                  AND TABLE_SCHEMA = 'dbo'
                                  AND TABLE_NAME NOT IN ('Orders', 'Customers', 'OrderProduct', '__EFMigrationsHistory', 'TopProducts');";

                var tableNames = connection.Query<string>(query);

               
                    Console.WriteLine($"\nInnehåll i tabell: Categories");

                    try
                    {
                        string selectQuery = $"SELECT * FROM Categories";
                        var rows = connection.Query(selectQuery).Cast<IDictionary<string, object>>();

                        foreach (var row in rows)
                        {
                            // Exkludera fält med null-värden
                            var nonNullFields = row.Where(kv => kv.Value != null);
                            Console.WriteLine(string.Join("| ", nonNullFields.Select(kv => $"{kv.Key}: {kv.Value}")));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fel vid hämtning från tabell Categories: {ex.Message}");
                    }

                    Console.WriteLine(new string('-', 50));
                
            }


            Console.Write("Ange ID för kategorin att ta bort: ");
                    string input = Console.ReadLine();

                    int deleteCategoryId;
                    if (int.TryParse(input, out deleteCategoryId))
                    {
                        Console.WriteLine($"Du angav ID: {deleteCategoryId}");
                        if (deleteCategoryId > 0 && deleteCategoryId < 5)
                        {
                            Console.WriteLine("Du kan ej ta bort en BAS kategori, vänligen välj ett annat ID.");
                            Console.ReadKey(true);
                            return;
                        }
                        using (var connection = new SqlConnection(connString))
                        {
                            connection.Open();


                            string getNameQuery = "SELECT Name FROM Categories WHERE CategoryId = @CategoryId";
                            string categoryName = connection.QuerySingleOrDefault<string>(getNameQuery, new { CategoryId = deleteCategoryId });

                            if (categoryName == null)
                            {
                                Console.WriteLine($"Ingen kategori hittades med ID {deleteCategoryId}.");
                                return;
                            }


                            string deleteCategoryQuery = "DELETE FROM Categories WHERE CategoryId = @CategoryId";
                            int rowsAffected = connection.Execute(deleteCategoryQuery, new { CategoryId = deleteCategoryId });




                            string dropTableQuery = $"DROP TABLE {categoryName}";
                            try
                            {
                                connection.Execute(dropTableQuery);
                                Console.WriteLine($"Kategorin med ID {deleteCategoryId} och namn {categoryName} har tagits bort.");
                                Thread.Sleep(5000);
                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine($"Kunde inte radera kategorin {categoryName}: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt ID. Försök igen.");

                    }
                }
            

        public static void UpdateCategory()
        {
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();

                string query = @"
                                SELECT TABLE_NAME
                                FROM INFORMATION_SCHEMA.TABLES
                                WHERE TABLE_TYPE = 'BASE TABLE'
                                  AND TABLE_SCHEMA = 'dbo'
                                  AND TABLE_NAME NOT IN ('Orders', 'Customers', 'OrderProduct', '__EFMigrationsHistory', 'TopProducts');";

                var tableNames = connection.Query<string>(query);

                
                    Console.WriteLine($"\nInnehåll i tabell: Categories");

                    try
                    {
                        string selectQuery = $"SELECT * FROM Categories";
                        var rows = connection.Query(selectQuery).Cast<IDictionary<string, object>>();

                        foreach (var row in rows)
                        {
                            // Exkludera fält med null-värden
                            var nonNullFields = row.Where(kv => kv.Value != null);
                            Console.WriteLine(string.Join("| ", nonNullFields.Select(kv => $"{kv.Key}: {kv.Value}")));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fel vid hämtning från tabell Categories: {ex.Message}");
                    }

                    Console.WriteLine(new string('-', 50));
                
            }
            string tableName;
            do
            {
                Console.Write("Vänligen ange kategorins namn: ");
                tableName = Console.ReadLine();
                if (tableName == "Games" || tableName == "GraphicsCard" || tableName == "Monitors" || tableName == "Merch")
                {
                    Console.WriteLine("Bas kategorier går ej att redigera, vänligen välj någon annan kategori. Klicka ENTER för att fortsätta...");
                    Console.ReadKey(true);

                }
            } while (tableName == "Games" || tableName == "GraphicsCard" || tableName == "Monitors" || tableName == "Merch");


            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine("\n================================================================================");
                Console.WriteLine("Välj aktivitet genom att trycka på motsvarande siffra på tangentbordet.");
                Console.WriteLine("================================================================================");
                Console.WriteLine();
                foreach (int i in Enum.GetValues(typeof(Update)))
                {
                    Console.WriteLine(i + ". " + Enum.GetName(typeof(Update), i).Replace('_', ' '));
                }
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int nr))
                {
                    switch ((Update)nr)   
                    {
                        case Update.Lägga_till_kolumn:
                            using(var connection = new SqlConnection(connString))
                            {
                                connection.Open();
                                Console.Write("Ange kolumnnamn: ");
                                string columnName = Console.ReadLine();
                                Console.Write("Ange datatyp: ");
                                string dataType = Console.ReadLine();
                                string alterTable = $"ALTER TABLE {tableName} ADD {columnName} {dataType};";

                                connection.Execute(alterTable);

                                Console.WriteLine("Kolumnen har lagts till.");
                                Thread.Sleep(2500);          
  
                            }
                            break;

                        case Update.Ta_bort_kolumn:
                            using (var connection = new SqlConnection(connString))
                            {
                                try
                                {
                                    connection.Open();
        
                                    string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
                                    var columns = connection.Query<string>(query, new { TableName = tableName }).ToList();
                                    
                                    
                                    Console.WriteLine("\nKOLUMNER");
                                    Console.WriteLine("¯¯¯¯¯¯¯¯¯");
                                    foreach (var column in columns)
                                    {
                                        Console.WriteLine(column);
                                    }
                                    
                                    Console.Write("\nAnge kolumnnamn att ta bort: ");
                                    string droppedColumn = Console.ReadLine();
                                   
                                    if (!columns.Contains(droppedColumn))
                                    {
                                        Console.WriteLine("Kolumnen finns inte i kategorin. ENTER för att gå vidare...");
                                        Console.ReadKey(true);
                                        return; 
                                    }
                                  
                                    string dropColumnQuery = $"ALTER TABLE {tableName} DROP COLUMN {droppedColumn};";
                                    connection.Execute(dropColumnQuery);

                                    Console.WriteLine($"Kolumnen {droppedColumn} har tagits bort.");
                                    Thread.Sleep(2000);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Ett fel inträffade: {ex.Message}");
                                }
                                finally
                                {
                                    connection.Close();
                                }
                            }

                            break;
                        case Update.Byta_namn_på_kolumn:
                            using( var connection = new SqlConnection(connString))
                            {
                                connection.Open();

                                string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
                                var columns = connection.Query<string>(query, new { TableName = tableName }).ToList();


                                Console.WriteLine("\nKOLUMNER");
                                Console.WriteLine("¯¯¯¯¯¯¯¯¯");
                                foreach (var column in columns)
                                {
                                    Console.WriteLine(column);
                                }

                                Console.Write("\nAnge namn på kolumn som ska bytas: ");
                                string currentColumnName = Console.ReadLine();
                                if (!columns.Contains(currentColumnName))
                                {
                                    Console.WriteLine("Kolumnen finns inte i kategorin. ENTER för att gå vidare...");
                                    Console.ReadKey(true);
                                    return;
                                }

                                Console.Write("\nAnge nytt kolumnnamn: ");
                                string newColumnName = Console.ReadLine();

                                try
                                {
                                    string alterColumnName = $"EXEC sp_rename '{tableName}.{currentColumnName}', '{newColumnName}', 'COLUMN'";
                                    connection.Execute(alterColumnName);
                                    Console.WriteLine($"Kolumnnamnet har ändrats till {newColumnName}.");
                                    Thread.Sleep(2500);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Ett fel inträffade: {ex.Message}");
                                }

                            }
                            break;
                        case Update.Byta_namn_på_tabell:
                            using(var connection = new SqlConnection(connString))
                            {
                                Console.WriteLine("Nuvarande kategorinamn: " + tableName);
                                connection.Open();
                                Console.Write("Ange nytt namn på katergorin: ");
                                string newTableName = Console.ReadLine();
                               
                                try
                                {
                                    string alterTableName = $"EXEC sp_rename '{tableName}', '{newTableName}';";
                                    connection.Execute(alterTableName);
                                    Console.WriteLine($"Kategorinamnet har ändrats till {newTableName}.");
                                    Thread.Sleep(2500);

                                    string alterNameInCategories = $"UPDATE Categories SET Name = '{newTableName}' WHERE Name = '{tableName}';";
                                    connection.Execute(alterNameInCategories);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Ett fel inträffade: {ex.Message}");
                                    Console.ReadKey(true);
                                }
                            }
                            break;
                        case Update.Avsluta:     
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Fel nummer");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Fel inmatning");
                    Console.ReadKey(true);
                }
            }


        }
        public static void AdministrateCustomers()
        {

            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine("\n================================================================================");
                Console.WriteLine("Välj aktivitet genom att trycka på motsvarande siffra på tangentbordet.");
                Console.WriteLine("================================================================================");
                Console.WriteLine();
                foreach (int i in Enum.GetValues(typeof(CustomerSettings)))
                {
                    Console.WriteLine(i + ". " + Enum.GetName(typeof(CustomerSettings), i).Replace('_', ' '));
                }
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int nr))
                {
                    switch ((CustomerSettings)nr)
                    {
                        case CustomerSettings.Radera_Kund:
                           
                            using (var myDb = new ShopDbContext())
                            {
                                Console.WriteLine("\nID \t Namn");
                                Console.WriteLine("ˉˉˉˉˉˉˉˉˉˉˉˉˉˉ");
                                foreach(var customer in myDb.Customers)
                                {
                                    Console.WriteLine(customer.CustomerId + "\t" + customer.Name);
                                }
                                Console.Write("\nAnge kundId: ");
                                int customerId = int.Parse(Console.ReadLine());
                                var deleteCustomer = myDb.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                                
                                if(deleteCustomer != null)
                                {
                                    myDb.Customers.Remove(deleteCustomer);
                                    myDb.SaveChanges();
                                    Console.WriteLine("Kunden med ID:" + customerId + " har tagits bort.");
                                    Thread.Sleep(4000);
                                }
                                else
                                {
                                    Console.WriteLine("Ingen kund med ID: " + customerId + " hittades.");
                                    Thread.Sleep(4000);
                                }
                            }
                            
                            break;
                        case CustomerSettings.Uppdatera_Kund:
                            using(var myDb = new ShopDbContext())
                            {
                                foreach(var customer in myDb.Customers)
                                {
                                    Console.WriteLine(customer.CustomerId + "\t" + customer.Name);
                                }
                                Console.Write("\nAnge KundID: ");
                                int customerId = int.Parse(Console.ReadLine());
                                var customerToUpdate = myDb.Customers.FirstOrDefault(c => c.CustomerId == customerId);

                                if(customerToUpdate != null)
                                {
                                    Console.WriteLine("Vilken kunduppgift vill du uppdatera");
                                    foreach (int i in Enum.GetValues(typeof(CustomerInfo)))
                                    {
                                        Console.WriteLine(i + ". " + Enum.GetName(typeof(CustomerInfo), i));
                                    }
                                    Console.Write("\nAnge nummer:");
                                    int choice = int.Parse(Console.ReadLine());

                                    switch (choice)
                                    {
                                        case 1:
                                            Console.Write("Ange nytt namn: ");
                                            string newName = Console.ReadLine();

                                            customerToUpdate.Name = newName;
                                            Console.WriteLine("Kundens namn har uppdaterats till: " + newName);
                                            myDb.SaveChanges();
                                            Thread.Sleep(4000);
                                            break;
                                        case 2:
                                            Console.Write("Ange ny gata: ");
                                            string newStreet = Console.ReadLine();

                                            customerToUpdate.Street = newStreet;
                                            Console.WriteLine("Kundens gata har uppdaterats till: " + newStreet);
                                            myDb.SaveChanges();
                                            Thread.Sleep(4000);
                                            break;
                                        case 3:
                                            Console.Write("Ange ny stad: ");
                                            string newCity = Console.ReadLine();

                                            customerToUpdate.City = newCity;
                                            Console.WriteLine("Kundens stad har uppdaterats till: " + newCity);
                                            myDb.SaveChanges();
                                            Thread.Sleep(4000);
                                            break;
                                        case 4:
                                            Console.Write("Ange nytt land: ");
                                            string newCountry = Console.ReadLine();

                                            customerToUpdate.Country = newCountry;
                                            Console.WriteLine("Kundens land har uppdaterats till: " + newCountry);
                                            myDb.SaveChanges();
                                            Thread.Sleep(4000);
                                            break;
                                        case 5:
                                            Console.Write("Ange nytt nummer: ");
                                            string newNumber = Console.ReadLine();

                                            customerToUpdate.Phone = newNumber;
                                            Console.WriteLine("Kundens mobilnummer har uppdaterats till: " + newNumber);
                                            myDb.SaveChanges();
                                            Thread.Sleep(4000);
                                            break;
                                        case 6:
                                            Console.Write("Ange nytt mail: ");
                                            string newMail = Console.ReadLine();

                                            customerToUpdate.Email = newMail;
                                            Console.WriteLine("Kundens mail har uppdaterats till: " + newMail);
                                            myDb.SaveChanges();
                                            Thread.Sleep(4000);
                                            break;
                                        case 7:
                                            Console.Write("Ange nytt ålder: ");
                                            int newAge = int.Parse(Console.ReadLine());

                                            customerToUpdate.Age = newAge;
                                            Console.WriteLine("Kundens mobilnummer har uppdaterats till: " + newAge);
                                            myDb.SaveChanges();
                                            Thread.Sleep(4000);
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ingen kund med ID:et: " + customerId + " hittades.");
                                    Console.ReadKey(true);
                                }
                                
                                
                            }
                            break;
                        case CustomerSettings.Beställningshistorik:
                            using (var myDb = new ShopDbContext())
                            {
                                foreach(var customer in myDb.Customers)
                                {
                                    Console.WriteLine(customer.CustomerId + "\t" + customer.Name);
                                }
                                Console.Write("\nAnge kundId: ");
                                int specificCustomerId = int.Parse(Console.ReadLine());

                                var customerOrderData = myDb.Customers
                                                        .Where(c => c.CustomerId == specificCustomerId)  
                                                        .SelectMany(customer => customer.Orders, (customer, order) => new
                                                        {
                                                            customer.CustomerId,
                                                            order.OrderId,
                                                            order.OrderDate
                                                        }).ToList();

                                foreach (var item in customerOrderData)
                                {
                                    Console.WriteLine($"CustomerId: {item.CustomerId}, OrderId: {item.OrderId}, OrderDate: {item.OrderDate}");
                                }
                                Console.WriteLine("\nENTER för att gå tillbaka...");
                                Console.ReadKey(true);
                                
                            }
                            break;
                        case CustomerSettings.Avsluta:
                            return;
                    }
                }
            }
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
                    Console.ReadKey(true);
                }
            }
        }

        public static void DeleteProduct()
        {
            using(var myDb = new ShopDbContext())
            {
                foreach (var product in myDb.Products)
                {
                    Console.WriteLine(product.ProductId + "\t" + product.Name + "\t" + product.Price);
                }

                Console.Write("\nAnge ProduktID: ");
                int productId = int.Parse(Console.ReadLine());

                var deleteProduct = myDb.Products.FirstOrDefault(p => p.ProductId == productId);

                if(deleteProduct != null)
                {
                    myDb.Products.Remove(deleteProduct);

                    myDb.SaveChanges();

                    Console.WriteLine($"Produkten med ID {productId} har tagits bort.");
                    Thread.Sleep(3000);
                }
                else
                {
                    Console.WriteLine($"Ingen produkt med ID {productId} hittades.");
                    Thread.Sleep(3000);
                }

            }
        }
        public static void UpdateProduct()
        {
            using (var myDb = new ShopDbContext())
            {
                foreach (var product in myDb.Products)
                {
                    Console.WriteLine(product.ProductId + "\t" + product.Name + "\t" + product.Price);
                }
                Console.Write("Ange ProductId för produkten som ska kolla kategorin: ");
                int productId = int.Parse(Console.ReadLine());

                var productCatAndId = myDb.Products
                                     .Where(p => p.ProductId == productId)
                                     .Select(p => new { p.ProductId, p.CategoryId })
                                     .FirstOrDefault();

                var updateProduct = myDb.Products.FirstOrDefault(p => p.ProductId == productId);
                if (updateProduct == null)
                {
                    Console.WriteLine($"Ingen produkt med ID {productId} hittades.");
                    Thread.Sleep(3000);
                    return;
                }



                if (productCatAndId != null)
                {
                    var category = myDb.Categories
                                         .Where(c => c.CategoryId == productCatAndId.CategoryId)
                                         .FirstOrDefault();

                    if (category != null)
                    {
                        Console.WriteLine($"Produktens kategori: {category.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Kategori inte funnen.");
                        Thread.Sleep(3000);
                    }
                    Console.Write("Ange nytt namn för produkten: ");
                    updateProduct.Name = Console.ReadLine();
                    
                    Console.Write("Ange nytt pris för produkten: ");
                    updateProduct.Price = decimal.Parse(Console.ReadLine());
                    
                    Console.Write("Ange ny beskrivning för produkten: ");
                    updateProduct.Description = Console.ReadLine();

                    Console.Write("Ange nytt antal i lager: ");
                    updateProduct.Stock = int.Parse(Console.ReadLine());

                    Console.Write("Ange ny leverantör: ");
                    updateProduct.Supplier = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"Ingen produkt med ID {productId} hittades.");
                    Thread.Sleep(3000);
                }

                switch(productCatAndId.CategoryId)
                {
                    case 1:
                        var game = updateProduct as Game;
                        Console.Write("Ange genre: ");
                        game.Genre = Console.ReadLine();

                        Console.Write("Ange utgivare: ");
                        game.Publisher = Console.ReadLine();

                        Console.Write("Ange plattform: ");
                        game.Platform = Console.ReadLine();
                        Console.WriteLine($"Produkten har uppdaterats.");
                        Console.ReadKey(true);

                        break;
                    case 2:
                        var graphicsCard = updateProduct as GraphicsCard;
                        Console.Write("Ange märke: ");
                        graphicsCard.Brand = Console.ReadLine();

                        Console.Write("Ange mängd VRAM: ");
                        graphicsCard.Vram = Console.ReadLine();

                        Console.Write("Ange strömförbrukning: ");
                        graphicsCard.PowerConsumption = Console.ReadLine();

                        Console.Write("Ange rekommenderad nätaggreggat: ");
                        graphicsCard.RecommendedPSU = Console.ReadLine();
                        Console.WriteLine($"Produkten har uppdaterats.");
                        Console.ReadKey(true);

                        break;
                    case 3:
                        var monitor = updateProduct as Models.Monitor;
                        Console.Write("Ange Märke: ");
                        monitor.Brand = Console.ReadLine();

                        Console.Write("Ange skärmens uppdateringsfrekvens: ");
                        monitor.RefreshRate = Console.ReadLine();

                        Console.Write("Ange skärmens upplösning: ");
                        monitor.Resolution = Console.ReadLine();

                        Console.Write("Ange skärmens paneltyp: ");
                        monitor.PanelType = Console.ReadLine();

                        Console.Write("Ange skärmstorlek: ");
                        monitor.Size = Console.ReadLine();
                        Console.WriteLine($"Produkten har uppdaterats.");
                        Console.ReadKey(true);
                        break;
                    case 4:
                        var merch = updateProduct as Models.Merch;
                        Console.Write("Ange storlek: ");
                        merch.Size = Console.ReadLine();

                        Console.Write("Ange material: ");
                        merch.Material = Console.ReadLine();
                        Console.WriteLine($"Produkten har uppdaterats.");
                        Console.ReadKey(true);
                        break;
                    default:
                        Console.WriteLine("Ingen kategori med detta ID existerar");
                        return;
                }
                myDb.SaveChanges();
            }
        }
        public static void UpdatePopularProducts()
        {
            using (var myDb = new ShopDbContext())
            {
                
                var TopProducts = myDb.TopProducts
                    .Join(myDb.Products, tp => tp.ProductId, p => p.ProductId, (tp, p) => new { tp.Ranking, p.Name })
                    .OrderBy(tp => tp.Ranking)
                    .ToList();

                
                Console.WriteLine("Aktuella topprodukter:");
                for (int i = 0; i < TopProducts.Count; i++)
                {
                    Console.WriteLine($"{TopProducts[i].Ranking} \t {TopProducts[i].Name}");
                }

                Console.Write("\nVilken topprodukt vill du byta ut? (1-3): ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 3)
                {
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    return;
                }

                
                Console.WriteLine("\n==========================");
                Console.WriteLine("Tillgängliga produkter:");
                Console.WriteLine("==========================\n");

                foreach (var product in myDb.Products)
                {
                    Console.WriteLine($"{product.ProductId} \t {product.Name.PadRight(26)} \t {product.Price}");
                }

                Console.Write("\nAnge nya topproduktens ID: ");
                if (!int.TryParse(Console.ReadLine(), out int newTopProductId))
                {
                    Console.WriteLine("Ogiltigt ID. Försök igen.");
                    return;
                }

                var newTopProduct = myDb.Products.FirstOrDefault(p => p.ProductId == newTopProductId);
                if (newTopProduct == null)
                {
                    Console.WriteLine("Produkten med det angivna ID:t hittades inte.");
                    return;
                }
    
                var topProductToUpdate = myDb.TopProducts.FirstOrDefault(tp => tp.Ranking == choice);
                if (topProductToUpdate != null)
                {
                    topProductToUpdate.ProductId = newTopProductId;
                    myDb.SaveChanges();
                    Console.WriteLine("Topp-produkt utbytt.");
                    Thread.Sleep(3000);
                }
                else
                {
                    Console.WriteLine("Misslyckades med att uppdatera topprodukten.");
                }
            }
        }        
    }
}
