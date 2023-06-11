namespace PizzaCuDeToateAPI.Models;

public class StripeInvoiceItem
{
    public string InvoiceItemId { get; set; }
    public string CustomerId { get; set; }

    public string InvoiceId { get; set; }

    public string Description { get; set; }

    public long Amount { get; set; }

    public long Quantity { get; set; }

    public bool Discountable { get; set; }

    public string Currency { get; set; }

    public StripeInvoiceItem()
    {
        
    }

    public StripeInvoiceItem(string invoiceItemId, string customerId, string invoiceId, string description, long amount, long quantity, bool discountable, string currency)
    {
        InvoiceItemId = invoiceItemId;
        CustomerId = customerId;
        InvoiceId = invoiceId;
        Description = description;
        Amount = amount;
        Quantity = quantity;
        Discountable = discountable;
        Currency = currency;
    }
}