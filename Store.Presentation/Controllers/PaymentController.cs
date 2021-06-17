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
        public async Task<IActionResult> CreatePayment(PayModel payment)
        { 
            await _paymentService.CreatePaymentAsync(payment);

            return Ok("Order is created");
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderModel orderModel)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            orderModel.UserId = Convert.ToInt64(userId);

            await _paymentService.CreateOrderAsync(orderModel);

            return Ok("Order is created");
        }

        [HttpPost("CreateOrderItem")]
        public async Task<IActionResult> CreateOrderItem(List<OrderItemModel> orderItemModel)
        {
            await _paymentService.CreateOrderItemAsync(orderItemModel);

            return Ok();
        }
    }
}
