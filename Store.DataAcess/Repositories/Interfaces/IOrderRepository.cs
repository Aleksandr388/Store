using Store.DataAcess.Entities;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        public decimal GetOrderPrice(Order order);
    }
}
