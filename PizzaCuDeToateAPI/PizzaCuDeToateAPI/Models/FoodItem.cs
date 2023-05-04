namespace PizzaCuDeToateAPI.Models;

public class FoodItem
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Category Category { get; set; }

    public string Description { get; set; } = string.Empty;

    public double UnitPrice { get; set; }

    public List<StockItem> Ingredients { get; set; } = new List<StockItem>();

    public List<string> Images { get; set; } = new List<string>();

    public string Logo { get; set; }

    public FoodItem()
    {
        
    }

    public FoodItem(string name, Category category, string description, double unitPrice, List<string> images, string logo)
    {
        Name = name;
        Category = category;
        Description = description;
        UnitPrice = unitPrice;
        Images = images;
        Logo = logo;
    }
}