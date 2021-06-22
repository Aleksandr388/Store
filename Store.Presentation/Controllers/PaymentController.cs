using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(PayModel payment, long userId)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            userId = Convert.ToInt64(id);

            await _paymentService.CreatePaymentAsync(payment, userId);

            return Ok("Order is created");
        }
    }
}
