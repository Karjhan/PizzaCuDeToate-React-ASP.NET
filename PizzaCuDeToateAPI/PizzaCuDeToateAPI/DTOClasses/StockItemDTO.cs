using Microsoft.Build.Framework;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.DTOClasses;

public class StockItemDTO
{
    public string Name { get; set; } 
    
    public int CategoryId { get; set; }
    public bool IsIngredient { get; set; }
    
    public string QuantityPerUnit { get; set; } 
    
    public List<int> MealIds { get; set; } 
    public double UnitPrice { get; set; }
    
    public int UnitsInStock { get; set; }

    public string Logo { get; set; } 
}