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

    public PaymentType PaymentType { get; set; }

    public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public Order()
    {
    }

    public Order(string firstName, string lastName, string address, string phoneNumber, ApplicationUser customer, PaymentType paymentType)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PhoneNumber = phoneNumber;
        OrderPlacedTime = DateTime.Now.ToUniversalTime();
        Customer = customer;
        PaymentType = paymentType;
    }
}