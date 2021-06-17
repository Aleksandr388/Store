using Store.DataAcess.Entities;
using System.Collections.Generic;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public decimal GetOrderPrice(Order model);
    }
}
