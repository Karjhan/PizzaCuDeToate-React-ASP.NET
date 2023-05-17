
namespace PizzaCuDeToateAPI.DTOClasses;

public class AddOrderDTO
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    
    public DateTime OrderPlacedTime { get; set; }

    public string UserEmail { get; set; } = string.Empty;
    
}