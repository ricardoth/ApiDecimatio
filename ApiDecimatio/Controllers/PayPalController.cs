﻿using Decimatio.Domain.IntegrationEntities;

namespace Decimatio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly IPayPalService _payPalService;

        public PayPalController(IPayPalService payPalService)
        {
            _payPalService = payPalService;
        }

        [HttpPost("CreateAccessToken")]
        public async Task<IActionResult> CreateAccessToken()
        {
            var result = await _payPalService.CreateAccessToken();
            return Ok(result);
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(Order order)
        {
            var result = await _payPalService.CreatePayment(order);
            return Ok(result);
        }
    }
}
