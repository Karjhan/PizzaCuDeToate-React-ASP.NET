namespace PizzaCuDeToateAPI.DTOClasses;

public class UpdateStockItemDTO
{
    public string Name { get; set; } 
    
    public bool IsIngredient { get; set; }
    
    public string QuantityPerUnit { get; set; } 
    
    public double UnitPrice { get; set; }
    
    public int UnitsInStock { get; set; }

    public string Logo { get; set; } 
}