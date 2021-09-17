using Microsoft.Extensions.Options;
using Store.DataAcess.Entities;
using Store.DataAcess.Options;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.Interfaces;

namespace Store.DataAcess.Repositories.DapperRepositories
{
    public class PaymentDapperRepository : BaseDapperRepository<Payment>, IPaymentRepository
    {
        private readonly ConnectionStrings _connectionString;

        public PaymentDapperRepository(IOptions<ConnectionStrings> options) : base(options)
        {
            _connectionString = options.Value;
        }
    }
}
