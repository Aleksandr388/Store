using Microsoft.EntityFrameworkCore;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.Interfaces;
using Store.DataAcess.StoreContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.EFRepositories
{
    public class OrderRepositiry : BaseEFRepository<Order>, IOrderRepository
    {
        public OrderRepositiry(ShopDbContext context) : base(context)
        {
        }

        public decimal GetOrderPrice(Order model)
        {
            var amount = model.OrderItems.Sum(x => x.Amount * x.Count);

            return amount;
        }

        public override async Task<Order> GetByIdAsync(long id)
        {
            var result = await _dbSet
                .Include(x => x.OrderItems)
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
