namespace PizzaCuDeToateAPI.DTOClasses;

public class AddFoodItemDTO
{
    public string Name { get; set; }
    
    public int CategoryId { get; set; }
    
    public string Description { get; set; }

    public double UnitPrice { get; set; }
    
    public List<int> IngredientsIds { get; set; }

    public List<string> Images { get; set; }

    public string Logo { get; set; }
}