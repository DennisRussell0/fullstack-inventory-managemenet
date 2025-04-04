using System;
using InventoryManagementBackend.Entities;

namespace InventoryManagementBackend.Interfaces;

public interface IDatabaseConnector
{
    public List<Product> RetrieveProducts();
    public Product? RetrieveProductById(int id);
    public bool DeleteProduct(int id);
    public bool PostProduct(Product newProduct);
    public bool UpdateProduct(Product updatedProduct);
}
