using System;

namespace InventoryManagementBackend.Entities;

public class Order(int id, float price, DateTime date, string? buyer, string? address, Product[]? products)
{
    public int Id { get; set; } = id;
    public float Price { get; set; } = price;
    public DateTime Date { get; set; } = date;
    public string? Buyer { get; set; } = buyer;
    public string? Address { get; set; } = address;
    public Product[]? Products { get; set; } = products;

    public void CancelOrder(){

    }
    public void UpdateStatus(){

    }
}
