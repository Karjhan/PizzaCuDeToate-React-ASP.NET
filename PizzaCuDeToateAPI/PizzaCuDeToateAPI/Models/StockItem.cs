namespace PizzaCuDeToateAPI.Models;

public class StockItem
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public Category Category { get; set; }
    
    public bool IsIngredient { get; set; }
    
    public string QuantityPerUnit { get; set; } = string.Empty;
    
    public double UnitPrice { get; set; }
    
    public int UnitsInStock { get; set; }
}