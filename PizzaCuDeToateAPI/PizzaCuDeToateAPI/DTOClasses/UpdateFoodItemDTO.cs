namespace PizzaCuDeToateAPI.DTOClasses;

public class UpdateFoodItemDTO
{
    public string Name { get; set; } 
    
    public string Description { get; set; }

    public double UnitPrice { get; set; }
    
    public string Logo { get; set; }
}