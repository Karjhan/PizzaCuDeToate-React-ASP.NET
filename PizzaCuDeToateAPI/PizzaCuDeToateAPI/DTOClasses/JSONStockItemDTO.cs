using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.DTOClasses;

public class JSONStockItemDTO
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public List<int> MealIds { get; set; } = new List<int>();

    public bool IsIngredient { get; set; }
    
    public string QuantityPerUnit { get; set; } = string.Empty;
    
    public double UnitPrice { get; set; }
    
    public int UnitsInStock { get; set; }
    
    public string Logo { get; set; }

    public void GetFromStockItem(StockItem originalStockItem)
    {
        Id = originalStockItem.Id;
        Name = originalStockItem.Name;
        CategoryId = originalStockItem.Category.Id;
        MealIds = originalStockItem.Meals.Select(stockItem => stockItem.Id).ToList();
        IsIngredient = originalStockItem.IsIngredient;
        QuantityPerUnit = originalStockItem.QuantityPerUnit;
        UnitPrice = originalStockItem.UnitPrice;
        UnitsInStock = originalStockItem.UnitsInStock;
        Logo = originalStockItem.Logo;
    }

}