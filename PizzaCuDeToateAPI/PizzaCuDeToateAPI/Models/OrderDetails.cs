using Microsoft.EntityFrameworkCore;

namespace PizzaCuDeToateAPI.Models;

public class OrderDetails
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    
    public Order Order { get; set; }

    public FoodItem FoodItem { get; set; }

    public int Quantity { get; set; }
}