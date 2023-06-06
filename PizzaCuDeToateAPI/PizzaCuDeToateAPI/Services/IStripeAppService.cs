using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Services;

public interface IStripeAppService
{
    Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomerDTO customer, CancellationToken cancellationToken);
    Task<StripePayment> AddStripePaymentAsync(AddStripePaymentDTO payment, CancellationToken cancellationToken);
}