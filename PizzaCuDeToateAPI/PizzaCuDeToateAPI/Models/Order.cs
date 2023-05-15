namespace PizzaCuDeToateAPI.Models;

public class Order
{
    public int OrderId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }
    
    public DateTime OrderPlacedTime { get; set; }

    public int UserId { get; set; }
}