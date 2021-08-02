using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Store.DataAcess.Entities;
using Store.DataAcess.Extensions;
using Store.DataAcess.Models;
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
            var amount = model.OrderItems.Sum(x => x.Price * x.Count);

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

        public async Task<(IEnumerable<Order>, int)> GetAllOrdersAsync(OrderFiltration filtration)
        {
            var orders = await _dbSet
                .Include(x => x.OrderItems)
                .Where(x => filtration.Description == null || x.Description.Contains(filtration.Description))
                .Where(x => filtration.OrderStatus == default || x.OrderStatus.Equals(filtration.OrderStatus))
                .Where(x => filtration.UserName == null || x.UserId.ToString().Contains(filtration.UserName))
                .OrderByField(filtration.SortOrder, filtration.IsAccesing)
                .Skip((filtration.PageNumber - 1) * filtration.PageSize)
                .Take(filtration.PageSize)
                .ToListAsync();

            var ordersCount = await _dbSet
               .Include(x => x.OrderItems)
               .AsNoTracking()
               .Where(x => filtration.Description == null || x.Description.Contains(filtration.Description))
               .Where(x => filtration.OrderStatus == default || x.OrderStatus.Equals(filtration.OrderStatus))
               .Where(x => filtration.UserName == null || x.UserId.ToString().Contains(filtration.UserName))
               .OrderByField(filtration.SortOrder, filtration.IsAccesing)
               .Skip((filtration.PageNumber - 1) * filtration.PageSize)
               .Take(filtration.PageSize)
               .CountAsync();

            var orderWithTotalAmount = (orders: orders, count: ordersCount);

            return orderWithTotalAmount;
        }
    }
}
