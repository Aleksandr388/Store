using Microsoft.Extensions.Options;
using Store.DataAcess.Entities;
using Store.DataAcess.Options;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.DapperRepositories.Interfaces;

namespace Store.DataAcess.Repositories.DapperRepositories
{
    public class OrderDapperRepository : BaseDapperRepository<Order>, IOrderDapperRepository
    {
        private readonly ConnectionStrings _connectionString;

        public OrderDapperRepository(IOptions<ConnectionStrings> options) : base(options)
        {
            _connectionString = options.Value;
        }
    }

}