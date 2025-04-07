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
            Storage = reader.GetInt32(reader.GetOrdinal("storage")),
            Price = reader.GetFloat(reader.GetOrdinal("price")),
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
                Storage = reader.GetInt32(reader.GetOrdinal("storage")),
                Price = reader.GetFloat(reader.GetOrdinal("price")),
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

    public List<Order> RetrieveOrders()
    {
        List<Order> orders = new List<Order>();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT * FROM orders", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            List<(int,int)> products = RetrieveProductsByOrderId(reader.GetInt32(reader.GetOrdinal("id")));

            if (products == null || products.Count == 0){
                continue;
            }

            var order = new Order
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Price = reader.GetFloat(reader.GetOrdinal("price")),
                Date = reader.GetDateTime(reader.GetOrdinal("date")),
                Buyer = reader.GetString(reader.GetOrdinal("buyer")),
                Address = reader.GetString(reader.GetOrdinal("address")),
                Products = products
            };

            orders.Add(order);
        }

        return orders;
    }

    public Order? RetrieveOrderById(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT * FROM orders WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            List<(int,int)> products = RetrieveProductsByOrderId(reader.GetInt32(reader.GetOrdinal("id")));

            if (products == null || products.Count == 0){
                return null;
            }

            return new Order
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Price = reader.GetFloat(reader.GetOrdinal("price")),
                Date = reader.GetDateTime(reader.GetOrdinal("date")),
                Buyer = reader.GetString(reader.GetOrdinal("buyer")),
                Address = reader.GetString(reader.GetOrdinal("address")),
                Products = products
            };
        }

        return null;
    }

    public bool PostOrder(Order newOrder)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("INSERT INTO orders (price, date, buyer, address) VALUES (@price, @date, @buyer, @address) RETURNING id", connection);
        command.Parameters.AddWithValue("@price", newOrder.Price);
        command.Parameters.AddWithValue("@date", newOrder.Date);
        command.Parameters.AddWithValue("@buyer", newOrder.Buyer ?? (Object) DBNull.Value);
        command.Parameters.AddWithValue("@address", newOrder.Address ?? (Object) DBNull.Value);

        var result = command.ExecuteScalar();
        if (result == null)
        {
            return false;
        }
        int orderId = Convert.ToInt32(result);

        foreach (var product in newOrder.Products)
        {
            using var productCommand = new NpgsqlCommand("INSERT INTO order_products (order_id, product_id, quantity) VALUES (@orderId, @productId, @quantity)", connection);
            productCommand.Parameters.AddWithValue("@orderId", orderId);
            productCommand.Parameters.AddWithValue("@productId", product.Item1);
            productCommand.Parameters.AddWithValue("@quantity", product.Item2);
            productCommand.ExecuteNonQuery();
        }

        return true;
    }
    
    public bool UpdateOrder(Order updatedOrder)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        // Update the order details
        using var command = new NpgsqlCommand("UPDATE orders SET price = @price, date = @date, buyer = @buyer, address = @address WHERE id = @id", connection);
        command.Parameters.AddWithValue("@price", updatedOrder.Price);
        command.Parameters.AddWithValue("@date", updatedOrder.Date);
        command.Parameters.AddWithValue("@buyer", updatedOrder.Buyer ?? (Object) DBNull.Value);
        command.Parameters.AddWithValue("@address", updatedOrder.Address ?? (Object) DBNull.Value);
        command.Parameters.AddWithValue("@id", updatedOrder.Id);

        int rowsAffected = command.ExecuteNonQuery();
        
        // Retrieve existing products for the order
        var existingProducts = RetrieveProductsByOrderId(updatedOrder.Id);

        // Update or insert products
        foreach (var product in updatedOrder.Products)
        {
            var existingProduct = existingProducts.FirstOrDefault(p => p.Item1 == product.Item1);
            if (existingProduct != default)
            {
                // Update quantity if the product already exists
                using var updateCommand = new NpgsqlCommand("UPDATE order_products SET quantity = @quantity WHERE order_id = @orderId AND product_id = @productId", connection);
                updateCommand.Parameters.AddWithValue("@quantity", product.Item2);
                updateCommand.Parameters.AddWithValue("@orderId", updatedOrder.Id);
                updateCommand.Parameters.AddWithValue("@productId", product.Item1);
                updateCommand.ExecuteNonQuery();
            }
            else
            {
                // Insert new product if it doesn't exist
                using var insertCommand = new NpgsqlCommand("INSERT INTO order_products (order_id, product_id, quantity) VALUES (@orderId, @productId, @quantity)", connection);
                insertCommand.Parameters.AddWithValue("@orderId", updatedOrder.Id);
                insertCommand.Parameters.AddWithValue("@productId", product.Item1);
                insertCommand.Parameters.AddWithValue("@quantity", product.Item2);
                insertCommand.ExecuteNonQuery();
            }
        }

        // Remove products that are no longer in the updated order
        foreach (var existingProduct in existingProducts)
        {
            if (!updatedOrder.Products.Any(p => p.Item1 == existingProduct.Item1))
            {
                using var deleteCommand = new NpgsqlCommand("DELETE FROM order_products WHERE order_id = @orderId AND product_id = @productId", connection);
                deleteCommand.Parameters.AddWithValue("@orderId", updatedOrder.Id);
                deleteCommand.Parameters.AddWithValue("@productId", existingProduct.Item1);
                deleteCommand.ExecuteNonQuery();
            }
        }


        return rowsAffected > 0;
    }

    public bool DeleteOrder(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        // Delete the order and its associated products
        using var command = new NpgsqlCommand("DELETE FROM orders WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        // First delete the associated products
        using var deleteProductsCommand = new NpgsqlCommand("DELETE FROM order_products WHERE order_id = @id", connection);
        deleteProductsCommand.Parameters.AddWithValue("@id", id);
        deleteProductsCommand.ExecuteNonQuery();


        // Execute the delete command for the order
        int rowsAffected = command.ExecuteNonQuery();



        return rowsAffected > 0;
    }

    public List<(int, int)> RetrieveProductsByOrderId(int orderId)
    {
        List<(int, int)> products = new List<(int, int)>();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT product_id, quantity FROM order_products WHERE order_id = @orderId", connection);
        command.Parameters.AddWithValue("@orderId", orderId);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            products.Add((reader.GetInt32(0), reader.GetInt32(1)));
        }

        return products;
    }
}
