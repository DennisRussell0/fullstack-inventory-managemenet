using System;

namespace InventoryManagementBackend.Entities;

public class Order
{
    public int Id { get; set; }
    public float Price { get; set; }
    public DateTime Date { get; set; }
    public string? Buyer { get; set; }
    public string? Address { get; set; }
    public List<(int, int)> Products { get; set; } = [];

    public Order(){ }
    public void CancelOrder(){

    }
    public void UpdateStatus(){

    }
}
