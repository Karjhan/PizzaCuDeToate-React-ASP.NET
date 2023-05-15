namespace PizzaCuDeToateAPI.Models;

public class Order
{
    public int OrderId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }
    
    public DateTime OrderPlacedTime { get; set; }

    public ApplicationUser UserId { get; set; }

    public Order(int orderId, string firstName, string lastName, string address, DateTime orderPlacedTime, ApplicationUser userId)
    {
        OrderId = orderId;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        OrderPlacedTime = orderPlacedTime;
        UserId = userId;
    }

    public Order(string firstName, string lastName, string address, DateTime orderPlacedTime, ApplicationUser userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        OrderPlacedTime = orderPlacedTime;
        UserId = userId;
    }
}