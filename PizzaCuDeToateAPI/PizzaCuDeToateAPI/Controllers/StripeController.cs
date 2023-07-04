using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Services;
using Stripe;

namespace PizzaCuDeToateAPI.Controllers
{
    [Route("api/stripe")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IStripeAppService _stripeService;
        private readonly IConfiguration _configuration;

        public StripeController(IStripeAppService stripeService, IConfiguration configuration)
        {
            _stripeService = stripeService;
            _configuration = configuration;
        }
        
        [HttpGet("customer/name={name}&email={email}")]
        public async Task<ActionResult<StripeCustomer>> AddStripeCustomer([FromRoute] string name, [FromRoute] string email, CancellationToken cancellationToken)
        {
            StripeCustomer? foundCustomer = await _stripeService.FindByNameAndEmailAsync(name, email, cancellationToken);
            if (foundCustomer is null)
            {
                return NotFound(new {result = "Not found"});
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
            return Ok(finalizedInvoice);
        }
        
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            try
            {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

                
                // validate webhook called by stripe only
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _configuration["StripeSettings:WebhookSecret"]);
                Console.WriteLine(stripeEvent);
                switch (stripeEvent.Type)
                {
                    case "customer.created":
                        var customer = stripeEvent.Data.Object as Customer;
                        // do work
                        Console.WriteLine("Customer created");
                        break;

                    case "customer.subscription.created":
                    case "customer.subscription.updated":
                    case "customer.subscription.deleted":
                    case "customer.subscription.trial_will_end":
                        var subscription = stripeEvent.Data.Object as Subscription;
                        // do work
                        Console.WriteLine("Customer modified");
                        break;

                    case "invoice.created":
                        var newinvoice = stripeEvent.Data.Object as Invoice;
                        // do work
                        Console.WriteLine("Invoice created");
                        break;

                    case "invoice.upcoming":
                    case "invoice.payment_succeeded":
                    case "invoice.payment_failed":
                        var invoice = stripeEvent.Data.Object as Invoice;
                        // do work
                        Console.WriteLine("Invoice modified");
                        break;

                    case "coupon.created":
                    case "coupon.updated":
                    case "coupon.deleted":
                        var coupon = stripeEvent.Data.Object as Coupon;
                        // do work
                        Console.WriteLine("Coupon modified");
                        break;
                }
                return Ok();
            }
            catch (StripeException ex)
            {
                //_logger.LogError(ex, $"StripWebhook: {ex.Message}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"StripWebhook: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
