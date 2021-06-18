using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.PaginationModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        public Task CreateAsync(OrderModel model);
        public Task<IEnumerable<OrderModel>> GetAllAsync();
        public Task<ResponseModel<OrderModel>> GetAllOrdersAsync(OrderFiltrationModel model);
    }
}
