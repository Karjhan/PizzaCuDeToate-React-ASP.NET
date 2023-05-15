namespace PizzaCuDeToateAPI.Models;

public class OrderDetails
{
    public int Id { get; set; }

    public Order OrderId { get; set; }

    public FoodItem FoodItemId { get; set; }

    public StockItem StockItemId { get; set; }

    public int TotalQuantityProducts { get; set; }
}