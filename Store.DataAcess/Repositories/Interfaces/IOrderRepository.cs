using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public decimal GetOrderPrice(Order model);
        public  Task<IEnumerable<Order>> GetAllOrdersAsync(OrderFiltration filtration);
    }
}
