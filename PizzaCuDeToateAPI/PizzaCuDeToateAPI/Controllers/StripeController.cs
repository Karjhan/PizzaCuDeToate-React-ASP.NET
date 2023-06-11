using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Services;

namespace PizzaCuDeToateAPI.Controllers
{
    [Route("api/stripe")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IStripeAppService _stripeService;

        public StripeController(IStripeAppService stripeService)
        {
            _stripeService = stripeService;
        }
        
        [HttpGet("customer/name={name}&email={email}")]
        public async Task<ActionResult<StripeCustomer>> AddStripeCustomer([FromRoute] string name, [FromRoute] string email, CancellationToken cancellationToken)
        {
            StripeCustomer? foundCustomer = await _stripeService.FindByNameAndEmailAsync(name, email, cancellationToken);
            if (foundCustomer is null)
            {
                return NotFound();
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
    }
}
