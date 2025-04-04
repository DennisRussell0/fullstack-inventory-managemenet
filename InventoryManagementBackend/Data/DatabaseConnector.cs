using Npgsql;
using InventoryManagementBackend.Entities;

namespace InventoryManagementBackend.Data;

public class DatabaseConnector
{
    private readonly string _connectionString = "Host=localhost;Username=postgres;Password=250505;Database=cerealdatabase";

    public List<Product> RetrieveProducts()
    {
        var products = new List<Product>();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT * FROM products", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var product = new Product
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
            Manufacturer = reader.GetString(reader.GetOrdinal("manufacturer")),
            Type = reader.GetString(reader.GetOrdinal("type")),
            Calories = reader.GetInt32(reader.GetOrdinal("calories")),
            Protein = reader.GetInt32(reader.GetOrdinal("protein")),
            Fat = reader.GetInt32(reader.GetOrdinal("fat")),
            Sodium = reader.GetInt32(reader.GetOrdinal("sodium")),
            Fiber = reader.GetFloat(reader.GetOrdinal("fiber")),
            Carbs = reader.GetFloat(reader.GetOrdinal("carbs")),
            Sugars = reader.GetInt32(reader.GetOrdinal("sugars")),
            Potassium = reader.GetInt32(reader.GetOrdinal("potassium")),
            Vitamins = reader.GetInt32(reader.GetOrdinal("vitamins")),
            Shelf = reader.GetInt32(reader.GetOrdinal("shelf")),
            Weight = reader.GetFloat(reader.GetOrdinal("weight")),
            Cups = reader.GetFloat(reader.GetOrdinal("cups")),
            Rating = reader.GetFloat(reader.GetOrdinal("rating")),
            ImagePath = reader.IsDBNull(reader.GetOrdinal("image_path")) ? null : reader.GetString(reader.GetOrdinal("image_path"))
        };

            
            products.Add(product);
        }

        return products;
    }
}