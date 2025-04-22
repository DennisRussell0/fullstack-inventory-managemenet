// Makes sure that the database connector implements the methods needed to interact with the database

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

    public List<Order> RetrieveOrders();
    public Order? RetrieveOrderById(int id);
    public bool PostOrder(Order newOrder);
    public bool UpdateOrder(Order updatedOrder);
    public bool DeleteOrder(int id);
    public List<ProductItem> RetrieveProductsByOrderId(int orderId);

}
