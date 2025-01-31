using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgiften_Webbshop
{
    internal class AsyncSQL
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog=LvlUpGames; persist security info=True; Integrated Security=True; TrustServerCertificate=True";
        public static async Task<int> SQLStatisticsAsync()
        {
            try
            {
                using (var connection = new SqlConnection(connString))
                {
                    await connection.OpenAsync();

                    string sqlQuery = @"
                            SELECT COUNT(o.OrderId) AS OrderCount
                            FROM Orders o
                            JOIN Customers c ON o.CustomerId = c.CustomerId
                            WHERE c.City = @City";

                    var parameters = new { City = "Eskilstuna" };

                    var orderCount = await connection.QuerySingleAsync<int>(sqlQuery, parameters);

                    return orderCount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade: {ex.Message}");
                return -1;
            }
        }
    }
}
