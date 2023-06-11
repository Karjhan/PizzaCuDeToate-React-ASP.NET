using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Repositories.FoodItemRepository;
using Stripe;

namespace PizzaCuDeToateAPI.Services;

public class StripeAppService : IStripeAppService
{
    private readonly ChargeService _chargeService;
    private readonly CustomerService _customerService;
    private readonly TokenService _tokenService;
    private readonly InvoiceService _invoiceService;
    private readonly InvoiceItemService _invoiceItemService;
    private readonly IFoodItemRepository _foodItemRepository;

    public StripeAppService(
        ChargeService chargeService,
        CustomerService customerService,
        TokenService tokenService,
        InvoiceService invoiceService,
        InvoiceItemService invoiceItemService,
        IFoodItemRepository foodItemRepository)
    {
        _chargeService = chargeService;
        _customerService = customerService;
        _tokenService = tokenService;
        _invoiceService = invoiceService;
        _invoiceItemService = invoiceItemService;
        _foodItemRepository = foodItemRepository;
    }
    
    public async Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomerDTO customer, CancellationToken cancellationToken)
    {
        TokenCreateOptions tokenOptions = new TokenCreateOptions
        {
            Card = new TokenCardOptions
            {
                Name = customer.Name,
                Number = customer.CreditCard.CardNumber,
                ExpYear = customer.CreditCard.ExpirationYear,
                ExpMonth = customer.CreditCard.ExpirationMonth,
                Cvc = customer.CreditCard.Cvc
            }
        };
        Token stripeToken = await _tokenService.CreateAsync(tokenOptions, null, cancellationToken);
        CustomerCreateOptions customerOptions = new CustomerCreateOptions
        {
            Name = customer.Name,
            Email = customer.Email,
            Source = stripeToken.Id
        };
        Customer createdCustomer = await _customerService.CreateAsync(customerOptions, null, cancellationToken);
        return new StripeCustomer(createdCustomer.Name, createdCustomer.Email,createdCustomer.Id);
    }

    public async Task<StripePayment> AddStripePaymentAsync(AddStripePaymentDTO payment, CancellationToken cancellationToken)
    {
        ChargeCreateOptions paymentOptions = new ChargeCreateOptions
        {
            Customer = payment.CustomerId,
            ReceiptEmail = payment.ReceiptEmail,
            Description = payment.Description,
            Currency = payment.Currency,
            Amount = payment.Amount
        };
        var createdPayment = await _chargeService.CreateAsync(paymentOptions, null, cancellationToken);
        return new StripePayment(
            createdPayment.CustomerId,
            createdPayment.ReceiptEmail,
            createdPayment.Description,
            createdPayment.Currency,
            createdPayment.Amount,
            createdPayment.Id);
    }

    public async Task<StripeInvoiceItem> AddStripeInvoiceItemAsync(AddStripeInvoiceItemDTO itemToAdd, CancellationToken cancellationToken)
    {
        var foodItemToBuy = _foodItemRepository.GetSingle(foodItem => foodItem.Name == itemToAdd.FoodItemName);
        if (foodItemToBuy is null)
        {
            return null;
        }
        var invoiceItemOptions = new InvoiceItemCreateOptions
        {
            Customer = itemToAdd.CustomerId,
            Quantity = itemToAdd.Quantity,
            UnitAmount = (long?)foodItemToBuy.UnitPrice,
            Discountable = itemToAdd.Discountable,
            Invoice = itemToAdd.InvoiceId,
            Currency = itemToAdd.Currency,
            Description = foodItemToBuy.Description,
        };
        var createdInvoiceItem = await _invoiceItemService.CreateAsync(invoiceItemOptions, null, cancellationToken);
        return new StripeInvoiceItem(
            createdInvoiceItem.Id,
            createdInvoiceItem.CustomerId,
            createdInvoiceItem.InvoiceId,
            createdInvoiceItem.Description,
            createdInvoiceItem.Amount,
            createdInvoiceItem.Quantity,
            createdInvoiceItem.Discountable,
            createdInvoiceItem.Currency
        );
    }

    public async Task<StripeInvoice> AddInvoiceAsync(AddStripeInvoiceDTO invoice, CancellationToken cancellationToken)
    {
        var invoiceOptions = new InvoiceCreateOptions
        {
            Customer = invoice.CustomerId,
        };
        var createdInvoice = await _invoiceService.CreateAsync(invoiceOptions, null, cancellationToken);
        return new StripeInvoice(
            createdInvoice.Id,
            createdInvoice.CustomerId,
            createdInvoice.HostedInvoiceUrl,
            createdInvoice.InvoicePdf
        );
    }

    public async Task<StripeInvoice> FinalizeInvoiceAsync(string invoiceId, CancellationToken cancellationToken)
    {
        var createdInvoice = await _invoiceService.FinalizeInvoiceAsync(invoiceId, null, null, cancellationToken);
        return new StripeInvoice(
            createdInvoice.Id,
            createdInvoice.CustomerId,
            createdInvoice.HostedInvoiceUrl,
            createdInvoice.InvoicePdf
        ); 
    }

    public async Task<StripeCustomer> FindByNameAndEmailAsync(string name, string email, CancellationToken cancellationToken)
    {
        var searchOptions = new CustomerSearchOptions
        {
            Query = $"name:'{name}' AND email:'{email}'",
            Limit = 1
        };
        var foundCustomer = await _customerService.SearchAsync(searchOptions, null, cancellationToken);
        if (foundCustomer.Data.Count < 1)
        {
            return null;
        }
        return new StripeCustomer(
            foundCustomer.Data[0].Name,
            foundCustomer.Data[0].Email,
            foundCustomer.Data[0].Id
        );
    }
}