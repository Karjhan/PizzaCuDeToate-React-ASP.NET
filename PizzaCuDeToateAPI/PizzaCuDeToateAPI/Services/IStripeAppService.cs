using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Services;

public interface IStripeAppService
{
    Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomerDTO customer, CancellationToken cancellationToken);
    Task<StripePayment> AddStripePaymentAsync(AddStripePaymentDTO payment, CancellationToken cancellationToken);
    Task<StripeInvoiceItem> AddStripeInvoiceItemAsync(AddStripeInvoiceItemDTO itemToAdd, CancellationToken cancellationToken);
    Task<StripeInvoice> AddInvoiceAsync(AddStripeInvoiceDTO invoice, CancellationToken cancellationToken);
    Task<StripeInvoice> FinalizeInvoiceAsync(string invoiceId, CancellationToken cancellationToken);
    Task<StripeCustomer> FindByNameAndEmailAsync(string name, string email, CancellationToken cancellationToken);
    Task<StripeInvoice> FindInvoiceById(string invoiceId, CancellationToken cancellationToken);
}