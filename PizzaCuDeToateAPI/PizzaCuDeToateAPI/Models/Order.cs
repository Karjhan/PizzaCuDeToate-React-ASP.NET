namespace PizzaCuDeToateAPI.Models;

public class Order
{
    public int Id { get; set; }

    public string FirstName { get; set; } 

    public string LastName { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }
    
    public DateTime OrderPlacedTime { get; set; }

    public ApplicationUser Customer { get; set; }

    public Order()
    {
    }

    public Order(string firstName, string lastName, string address,string phoneNumber, DateTime orderPlacedTime, ApplicationUser customer)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PhoneNumber = phoneNumber;
        OrderPlacedTime = orderPlacedTime;
        Customer = customer;
    }
}