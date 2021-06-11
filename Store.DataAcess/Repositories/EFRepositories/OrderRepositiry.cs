using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.Interfaces;
using Store.DataAcess.StoreContext;
using System.Linq;

namespace Store.DataAcess.Repositories.EFRepositories
{
    public class OrderRepositiry : BaseEFRepository<Order>, IOrderRepository
    {
        public OrderRepositiry(ShopDbContext context) : base(context)
        {
        }
        public decimal GetOrderPrice(Order order)
        {
            var price = order.OrderItems.Sum(x => x.PrintingEdition.Price * x.Count);

            return price;
        }
    }
}
