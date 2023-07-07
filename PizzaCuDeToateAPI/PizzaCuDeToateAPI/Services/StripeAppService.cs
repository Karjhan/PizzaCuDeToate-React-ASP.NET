using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            UnitAmount = (long?)foodItemToBuy.UnitPrice*100,
            Discountable = itemToAdd.Discountable,
            Invoice = itemToAdd.InvoiceId,
            Currency = itemToAdd.Currency,
            Description = foodItemToBuy.Name
            // Description = $"{foodItemToBuy.Name} - {foodItemToBuy.Description}",
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
        var address = invoice.Address.Split(",").Select((word) => word.Trim()).ToArray();
        var invoiceOptions = new InvoiceCreateOptions
        {
            Customer = invoice.CustomerId,
            ShippingDetails = new InvoiceShippingDetailsOptions()
            {
                Name = $"{invoice.FirstName}'s Hideout", 
                Phone = invoice.Phone.Trim(), 
                Address = new AddressOptions()
                {
                    Country = "Romania",
                    State = address[0], 
                    City = address[1], 
                    PostalCode = address[2], 
                    Line1 = address[3],
                    Line2 = address[4]
                }
            },
            Description = invoice.Description.Length > 0 ? invoice.Description.Trim() : null,
            Metadata = new Dictionary<string, string>(){{"FirstName", invoice.FirstName}, {"LastName", invoice.LastName}, {"AppUserName", invoice.AppUserName}, {"AppUserEmail", invoice.AppUserEmail}}
        };
        var createdInvoice = await _invoiceService.CreateAsync(invoiceOptions, null, cancellationToken);
        return new StripeInvoice(
            createdInvoice.Id,
            createdInvoice.CustomerId,
            createdInvoice.HostedInvoiceUrl,
            createdInvoice.InvoicePdf,
            createdInvoice.CustomerEmail,
            createdInvoice.CustomerName
        );
    }

    public async Task<StripeInvoice> FinalizeInvoiceAsync(string invoiceId, CancellationToken cancellationToken)
    {
        var createdInvoice = await _invoiceService.FinalizeInvoiceAsync(invoiceId, null, null, cancellationToken);
        return new StripeInvoice(
            createdInvoice.Id,
            createdInvoice.CustomerId,
            createdInvoice.HostedInvoiceUrl,
            createdInvoice.InvoicePdf,
            createdInvoice.CustomerEmail,
            createdInvoice.CustomerName
        ); 
    }

    public async Task<StripeInvoice> FindInvoiceById(string invoiceId, CancellationToken cancellationToken)
    {
        var foundInvoice = await _invoiceService.GetAsync(invoiceId, null, null, cancellationToken);
        return new StripeInvoice(
            foundInvoice.Id,
            foundInvoice.CustomerId,
            foundInvoice.HostedInvoiceUrl,
            foundInvoice.InvoicePdf,
            foundInvoice.CustomerEmail,
            foundInvoice.CustomerName
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