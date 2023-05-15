namespace PizzaCuDeToateAPI.Models;

public class OrderDetails
{
    public Order OrderId { get; set; }

    public FoodItem FoodItemId { get; set; }
    
    public int TotalQuantityProducts { get; set; }
}