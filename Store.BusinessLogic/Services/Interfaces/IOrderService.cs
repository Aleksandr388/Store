using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.PaginationModels;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        public Task CreateAsync(OrderModel model);
        public Task<ResponseModel<OrderModel>> GetAllOrdersAsync(OrderFiltrationModel model);
    }
}
