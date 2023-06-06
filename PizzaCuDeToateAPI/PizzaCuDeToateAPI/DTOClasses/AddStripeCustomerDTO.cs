namespace PizzaCuDeToateAPI.DTOClasses;

public class AddStripeCustomerDTO
{
    public string Email { get; set; }

    public string Name { get; set; }

    public AddStripeCardDTO CreditCard { get; set; }
}