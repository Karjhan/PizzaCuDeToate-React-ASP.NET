namespace PizzaCuDeToateAPI.Models;

public abstract class FoodItem
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public double UnitPrice { get; set; }

    public List<StockItem> Ingredients { get; set; }

    public List<Image> Images { get; set; }

    public Image Logo { get; set; }
}