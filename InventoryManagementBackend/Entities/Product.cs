using System;
using Microsoft.AspNetCore.SignalR;

namespace InventoryManagementBackend.Entities;

public class Product(int id, float price, int storage, string? name, string? manufacturer, string? type, int calories, int protein, int fat, int sodium, float fiber, float carbs, int sugars, int potassium, int vitamins, int shelf, float weight, float cups, float rating, string? imagePath)
{
    public int Id { get; set; } = id;
    public float Price { get; set; } = price;
    public int Storage { get; set; } = storage;
    public string? Name { get; set; } = name;
    public string? Manufacturer { get; set; } = manufacturer;
    public string? Type { get; set; } = type;
    public int Calories { get; set; } = calories;
    public int Protein { get; set; } = protein;
    public int Fat { get; set; } = fat;
    public int Sodium { get; set; } = sodium;
    public float Fiber { get; set; } = fiber;
    public float Carbs { get; set; } = carbs;
    public int Sugars { get; set; } = sugars;
    public int Potassium { get; set; } = potassium;
    public int Vitamins { get; set; } = vitamins;
    public int Shelf { get; set; } = shelf;
    public float Weight { get; set; } = weight;
    public float Cups { get; set; } = cups;
    public float Rating { get; set; } = rating;
    public string? ImagePath { get; set; } = imagePath;

    public void DeleteProduct(){

    }
    public void UpdateProduct(){
        
    }
}
