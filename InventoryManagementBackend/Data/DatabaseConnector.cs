using Npgsql;
using InventoryManagementBackend.Entities;

namespace InventoryManagementBackend.Data;

public class DatabaseConnector
{
    private readonly string _connectionString = "Host=localhost;Username=postgres;Password=250101;Database=cerealdatabase";

    public List<Product> GetProducts()
    {
        var products = new List<Product>();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT * FROM cereal", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            products.Add(new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetInt32(2),
                Storage = reader.GetInt32(3)
            });
        }

        return products;
    }
}