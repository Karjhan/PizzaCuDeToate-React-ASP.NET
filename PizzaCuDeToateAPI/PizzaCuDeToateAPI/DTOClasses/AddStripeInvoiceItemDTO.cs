namespace PizzaCuDeToateAPI.DTOClasses;

public class AddStripeInvoiceItemDTO
{
    public string CustomerId { get; set; }

    public string Currency { get; set; }

    public int FoodItemId { get; set; }

    public bool Discountable { get; set; }

    public int Quantity { get; set; }

    public string InvoiceId { get; set; }
}