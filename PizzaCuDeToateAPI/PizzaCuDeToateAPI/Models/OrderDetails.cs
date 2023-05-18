using Microsoft.EntityFrameworkCore;

namespace PizzaCuDeToateAPI.Models;

[Keyless]
public class OrderDetails
{
    public Order Order { get; set; }

    public FoodItem FoodItem { get; set; }

    public int Quantity { get; set; }
}