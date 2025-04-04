using System;
using Microsoft.AspNetCore.SignalR;

namespace InventoryManagementBackend.Entities;

public class Product
{
    public int Id { get; set; }
    public float Price { get; set; }
    public int Storage { get; set; }
    public string? Name { get; set; }
    public string? Manufacturer { get; set; }
    public string? Type { get; set; }
    public int Calories { get; set; }
    public int Protein { get; set; }
    public int Fat { get; set; }
    public int Sodium { get; set; }
    public float Fiber { get; set; }
    public float Carbs { get; set; }
    public int Sugars { get; set; }
    public int Potassium { get; set; }
    public int Vitamins { get; set; }
    public int Shelf { get; set; }
    public float Weight { get; set; }
    public float Cups { get; set; }
    public float Rating { get; set; }
    public string? ImagePath { get; set; }

    public Product(){ }
    public void DeleteProduct(){

    }
    public void UpdateProduct(){
        
    }
}
