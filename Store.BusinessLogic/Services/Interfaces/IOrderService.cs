using Store.BusinessLogic.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        public Task CreateAsync(OrderModel model);
        public Task<IEnumerable<OrderModel>> GetAllAsync();
    }
}
