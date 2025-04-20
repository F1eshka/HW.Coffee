using System;
using System.Data.SqlClient;

namespace CoffeeApp
{
    class Program
    {
        static string connectionString = @"Server=DESKTOP-Q4ID39U\SQLEXPRESS;Database=CoffeeMag;Trusted_Connection=True;";
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Пробуем подключиться к базе данных...");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("✅ Успешное подключение к базе данных!");

                    // Вызов методов
                    ShowAllCoffee(connection);
                    Console.WriteLine();

                    ShowCoffeeNames(connection);
                    Console.WriteLine();

                    ShowMinCost(connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Ошибка подключения: " + ex.Message);
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void ShowAllCoffee(SqlConnection connection)
        {
            string query = "SELECT * FROM CoffeeTypes";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("📦 Список усіх сортів кави:");
                while (reader.Read())
                {
                    Console.WriteLine($"[{reader["CoffeeId"]}] {reader["CoffeeTitle"]}, {reader["CountryMade"]}, " +
                                      $"{reader["CoffeeKind"]}, {reader["CoffeeInfo"]}, " +
                                      $"{reader["AmountGrams"]} г, {reader["PricePrime"]} грн");
                }
            }
        }

        static void ShowCoffeeNames(SqlConnection connection)
        {
            string query = "SELECT CoffeeTitle FROM CoffeeTypes";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("☕ Назви сортів кави:");
                while (reader.Read())
                {
                    Console.WriteLine($"- {reader["CoffeeTitle"]}");
                }
            }
        }

        static void ShowMinCost(SqlConnection connection)
        {
            string query = "SELECT MIN(PricePrime) AS MinPrice FROM CoffeeTypes";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                object result = cmd.ExecuteScalar();
                Console.WriteLine($"💰 Мінімальна собівартість кави: {result} грн");
            }
        }
    }
}
