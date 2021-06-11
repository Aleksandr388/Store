using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Create(PaymentModel payment)
        {
            await _paymentService.CreatePaymentAsync(payment);

            return Ok("Order is created");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(OrderModel orderModel)
        {
            await _paymentService.CreateOrderAsync(orderModel);

            return Ok("Order is created");
        }
    }
}
