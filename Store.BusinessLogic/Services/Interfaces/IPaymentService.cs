using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task CreateOrderAsync(OrderModel model);
        public Task CreatePaymentAsync(PayModel model);
        public Task CreateOrderItemAsync(List<OrderItemModel> model);
    }
}
