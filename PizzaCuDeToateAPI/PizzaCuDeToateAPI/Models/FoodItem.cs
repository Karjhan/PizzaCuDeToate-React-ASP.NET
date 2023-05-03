namespace PizzaCuDeToateAPI.Models;

public class FoodItem
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Category Category { get; set; }

    public string Description { get; set; } = string.Empty;

    public double UnitPrice { get; set; }

    public List<StockItem> Ingredients { get; set; }

    public List<string> Images { get; set; }

    public string Logo { get; set; }
}