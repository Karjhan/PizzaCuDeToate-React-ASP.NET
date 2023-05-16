using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.DTOClasses;

public class JSONOrderDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
    
    public DateTime OrderPlacedTime { get; set; }

    public string UserEmail { get; set; } = string.Empty;
    
    public void GetFromOrderDTO(Order origin)
    {
        Id = origin.Id;
        FirstName = origin.FirstName;
        LastName = origin.LastName;
        Address = origin.Address;
        OrderPlacedTime = origin.OrderPlacedTime;
        UserEmail = origin.Customer.Email;
    }
}