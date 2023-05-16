namespace PizzaCuDeToateAPI.Models;

public class Order
{
    public int Id { get; set; }

    public string FirstName { get; set; } 

    public string LastName { get; set; }

    public string Address { get; set; } 
    
    public DateTime OrderPlacedTime { get; set; }

    public ApplicationUser Customer { get; set; }

    public Order()
    {
    }

    public Order(string firstName, string lastName, string address, DateTime orderPlacedTime, ApplicationUser userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        OrderPlacedTime = orderPlacedTime;
        Customer = userId;
    }
}