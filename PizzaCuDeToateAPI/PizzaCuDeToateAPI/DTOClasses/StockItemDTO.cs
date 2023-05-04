using Microsoft.Build.Framework;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.DTOClasses;

public class StockItemDTO
{
    [Required]

    public string Name { get; set; } = string.Empty;
    
    public bool IsIngredient { get; set; }
    
    public string QuantityPerUnit { get; set; } = string.Empty;
    
    public double UnitPrice { get; set; }
    
    public int UnitsInStock { get; set; }

    public string Logo { get; set; } 
}