namespace PizzaCuDeToateAPI.Models;

public class OrderDetails
{
    public Order OrderId { get; set; }
    
    public Dictionary<FoodItem,int> ProductsAndQuantity { get; set; } = new Dictionary<FoodItem, int>();
}