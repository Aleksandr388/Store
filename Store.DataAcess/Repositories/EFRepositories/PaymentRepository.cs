using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.Interfaces;
using Store.DataAcess.StoreContext;
using System.Linq;

namespace Store.DataAcess.Repositories.EFRepositories
{
    public class PaymentRepository : BaseEFRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ShopDbContext context) : base(context)
        {
        }
    }
}
