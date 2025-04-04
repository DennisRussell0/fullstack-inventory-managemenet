using Npgsql;
using InventoryManagementBackend.Entities;
using InventoryManagementBackend.Interfaces;

namespace InventoryManagementBackend.Data;

public class DatabaseConnector : IDatabaseConnector
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

    public Product? RetrieveProductById(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT * FROM products WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Product{
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
        }

        return null;
    }
    public bool PostProduct(Product newProduct)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
    
        var query = @"INSERT INTO products (";
        var values = "VALUES (";
        var properties = typeof(Product).GetProperties(); 
        var parameters = new List<NpgsqlParameter>();

        foreach (var prop in properties)
        {
            if (prop.Name == "Id") continue;

            query += $"{prop.Name.ToLower()}, ";
            values += $"@{prop.Name.ToLower()}, ";

            var value = prop.GetValue(newProduct) ?? DBNull.Value;
            parameters.Add(new NpgsqlParameter($"@{prop.Name.ToLower()}", value));
        }

        query = query.TrimEnd(',', ' ') + ") ";
        values = values.TrimEnd(',', ' ') + ")";

        // Final SQL statement
        query += values;

        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddRange(parameters.ToArray());

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0;
    }

    public bool UpdateProduct(Product updatedProduct)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        // Initialize the base SQL query
        var query = @"UPDATE products SET ";
        // Use reflection to get the properties of the Product class
        var properties = typeof(Product).GetProperties();
        // List to hold parameters that will be added to the command
        var parameters = new List<NpgsqlParameter>();

        foreach (var prop in properties)
        {
            if (prop.Name == "Id") continue;
            // Add each property name and its value to the query, using parameterized queries to avoid SQL injection
            query += $"{prop.Name.ToLower()} = @{prop.Name.ToLower()},";
            // Get the value of the property from the updatedProduct object, or DBNull if the value is null
            var value = prop.GetValue(updatedProduct) ?? DBNull.Value;
            parameters.Add(new NpgsqlParameter($"@{prop.Name.ToLower()}", value));
        }

        query = query.TrimEnd(',', ' ') + " WHERE id = @id";
        parameters.Add(new NpgsqlParameter("@id", updatedProduct.Id));

        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddRange(parameters.ToArray());

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0;
    }

    public bool DeleteProduct(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("DELETE FROM products WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        int rowsAffected = command.ExecuteNonQuery();

        return rowsAffected > 0;
    }
}