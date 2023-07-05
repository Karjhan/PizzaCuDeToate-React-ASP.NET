namespace PizzaCuDeToateAPI.DTOClasses;

public class AddStripeInvoiceDTO
{
    public string CustomerId { get; set; }
    
    public string Address { get; set; }
    
    public string Phone { get; set; }

    public string Description { get; set; }
    
    public string FirstName { get; set; } 

    public string LastName { get; set; }
    
    public string AppUserName { get; set; }
    
    public string AppUserEmail { get; set; }
}