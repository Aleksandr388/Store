using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders([FromBody] OrderFiltrationModel orderModel)
        {
            var result = await _orderService.GetAllOrdersAsync(orderModel);

            return Ok(result);
        }
    }
}
