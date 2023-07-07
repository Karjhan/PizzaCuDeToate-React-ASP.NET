using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.Extensions.Configuration;
using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Repositories;
using PizzaCuDeToateAPI.Repositories.FoodItemRepository;
using PizzaCuDeToateAPI.Services;
using Stripe;
using Order = PizzaCuDeToateAPI.Models.Order;

namespace PizzaCuDeToateAPI.Controllers
{
    [Route("api/stripe")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IStripeAppService _stripeService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFoodItemRepository _foodItemRepository;
        private readonly IOrderRepository _orderRepository;

        public StripeController(IStripeAppService stripeService, IConfiguration configuration, IEmailService emailService, UserManager<ApplicationUser> userManager, IOrderRepository orderRepository, IFoodItemRepository foodItemRepository)
        {
            _stripeService = stripeService;
            _configuration = configuration;
            _emailService = emailService;
            _userManager = userManager;
            _orderRepository = orderRepository;
            _foodItemRepository = foodItemRepository;
        }
        
        [HttpGet("customer/name={name}&email={email}")]
        public async Task<ActionResult<StripeCustomer>> FindStripeCustomer([FromRoute] string name, [FromRoute] string email, CancellationToken cancellationToken)
        {
            StripeCustomer? foundCustomer = await _stripeService.FindByNameAndEmailAsync(name, email, cancellationToken);
            if (foundCustomer is null)
            {
                return NotFound(new {error = "Customer not found"});
            }
            return Ok(foundCustomer);
        }
        
        [HttpPost("customer")]
        public async Task<ActionResult<StripeCustomer>> AddStripeCustomer([FromBody] AddStripeCustomerDTO customer, CancellationToken cancellationToken)
        {
            StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(customer, cancellationToken);
            return Ok(createdCustomer);
        }
        
        [HttpPost("payment")]
        public async Task<ActionResult<StripePayment>> AddStripePayment([FromBody] AddStripePaymentDTO payment, CancellationToken cancellationToken)
        {
            StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(payment, cancellationToken);
            return Ok(createdPayment);
        }
        
        [HttpPost("invoiceItem")]
        public async Task<ActionResult<StripeInvoiceItem>> AddStripeInvoiceItem([FromBody] AddStripeInvoiceItemDTO itemToAdd, CancellationToken cancellationToken)
        {
            StripeInvoiceItem createdInvoiceItem = await _stripeService.AddStripeInvoiceItemAsync(itemToAdd, cancellationToken);
            if (createdInvoiceItem is null)
            {
                return NotFound("Either food item doesn't exist, or invoice item creation has failed!");
            }
            return Ok(createdInvoiceItem);
        }
        
        [HttpPost("invoice")]
        public async Task<ActionResult<StripeInvoice>> AddStripeInvoice([FromBody] AddStripeInvoiceDTO invoice, CancellationToken cancellationToken)
        {
            StripeInvoice createdInvoice = await _stripeService.AddInvoiceAsync(invoice, cancellationToken);
            return Ok(createdInvoice);
        }

        [HttpGet("invoice/finalize/id={invoiceId}")]
        public async Task<ActionResult<StripeInvoice>> FinalizeInvoice([FromRoute] string invoiceId, CancellationToken cancellationToken)
        {
            StripeInvoice finalizedInvoice = await _stripeService.FinalizeInvoiceAsync(invoiceId, cancellationToken);
            var message = new MailMessage(new[] { finalizedInvoice.CustomerEmail }, "Invoice confirmation email",
                $"Prepare for a world of tastes, {finalizedInvoice.CustomerName}!. Your order will start as soon as the payment is completed.\nClick on the following link to download your invoice: {finalizedInvoice.InvoicePDF!}");
            _emailService.SendEmail(message);
            return Ok(finalizedInvoice);
        }

        [HttpGet("invoice/id={invoiceId}")]
        public async Task<ActionResult<StripeInvoice>> FindStripeInvoice([FromRoute] string invoiceId, CancellationToken cancellationToken)
        {
            StripeInvoice foundInvoice = await _stripeService.FindInvoiceById(invoiceId, cancellationToken);
            return Ok(foundInvoice);
        }
        
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            try
            {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], "whsec_8873d176c622f839fa896e04ef5a282888ce6d8e3d6995c7e2b711caf8908ddb");
                switch (stripeEvent.Type)
                {
                    case "invoice.payment_succeeded":
                        CommandRunner.WebHookCount += 1;
                        if (CommandRunner.WebHookCount % 8 == 1)
                        {
                            var finalizedInvoice = (Invoice)stripeEvent.Data.Object;
                            ApplicationUser appUser = await _userManager.FindByEmailAsync(finalizedInvoice.Metadata["AppUserEmail"]);
                            string orderAddress = finalizedInvoice.ShippingDetails.Address.Country + ", " +
                                                  finalizedInvoice.ShippingDetails.Address.State + ", " +
                                                  finalizedInvoice.ShippingDetails.Address.City + ", " +
                                                  finalizedInvoice.ShippingDetails.Address.PostalCode + ", " +
                                                  finalizedInvoice.ShippingDetails.Address.Line1 + ", " +
                                                  finalizedInvoice.ShippingDetails.Address.Line2;
                            Regex regex = new Regex("invoice/(.*)/test");
                            string orderId = regex.Match(finalizedInvoice.InvoicePdf).Groups[0].ToString().Split("/")[1];
                            var newOrder = new Order(
                                finalizedInvoice.Metadata["FirstName"],
                                finalizedInvoice.Metadata["LastName"],
                                orderAddress,
                                finalizedInvoice.ShippingDetails.Phone,
                                appUser,
                                PaymentType.CASH);
                            var foodItems = finalizedInvoice.Lines.Select((item) => new OrderDetails()
                            {
                                FoodItem = _foodItemRepository.GetSingle((foodItem) => foodItem.Name == item.Description),
                                Quantity = (int)item.Quantity,
                                Order = newOrder
                            });
                            newOrder.OrderDetails.AddRange(foodItems);
                            var addedOrder = _orderRepository.AddSingle(newOrder);
                            if (addedOrder is null)
                            {
                                throw new NullReferenceException("Order was not saved!");
                            }
                            var message = new MailMessage(new[] { finalizedInvoice.CustomerEmail }, "Order successfully started",
                                $"Prepare for a world of tastes, {finalizedInvoice.Metadata["FirstName"]} {finalizedInvoice.Metadata["LastName"]}... Order with id {orderId} is on the way with lightning speed!");
                            _emailService.SendEmail(message);
                            // return Redirect($"{_configuration["Frontend_Url"]}/orderConfirmed/{orderId}/{finalizedInvoice.Metadata["FirstName"]}&{finalizedInvoice.Metadata["LastName"]}");
                            return Ok();
                        }
                        return Ok();
                }
                return Ok();
            }
            catch (StripeException ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
