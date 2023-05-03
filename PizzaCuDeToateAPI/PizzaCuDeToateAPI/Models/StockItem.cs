namespace PizzaCuDeToateAPI.Models;

public class StockItem
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public Category Category { get; set; }

    public List<FoodItem> Meals { get; set; } = new List<FoodItem>();

    public bool IsIngredient { get; set; }
    
    public string QuantityPerUnit { get; set; } = string.Empty;
    
    public double UnitPrice { get; set; }
    
    public int UnitsInStock { get; set; }
    
    public string Logo { get; set; }

    public StockItem()
    {
        
    }

    public StockItem(int id, string name, Category category, List<FoodItem> meals, bool isIngredient, string quantityPerUnit, double unitPrice, int unitsInStock, string logo)
    {
        Id = id;
        Name = name;
        Category = category;
        Meals = meals;
        IsIngredient = isIngredient;
        QuantityPerUnit = quantityPerUnit;
        UnitPrice = unitPrice;
        UnitsInStock = unitsInStock;
        Logo = logo;
    }
}