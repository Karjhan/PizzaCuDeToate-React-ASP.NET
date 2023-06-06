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
        
        [HttpPost("customer/add")]
        public async Task<ActionResult<StripeCustomer>> AddStripeCustomer([FromBody] AddStripeCustomerDTO customer, CancellationToken cancellationToken)
        {
            StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(customer, cancellationToken);
            return Ok(createdCustomer);
        }
        
        [HttpPost("payment/add")]
        public async Task<ActionResult<StripePayment>> AddStripePayment([FromBody] AddStripePaymentDTO payment, CancellationToken cancellationToken)
        {
            StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(payment, cancellationToken);
            return Ok(createdPayment);
        }
    }
}
