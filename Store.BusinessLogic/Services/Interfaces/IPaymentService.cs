using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task CreateOrderAsync(OrderModel model);
        public Task CreatePaymentAsync(PaymentModel model);
        public Task UpdateAsync(PaymentModel model);
        public Task DeleteAsync(PaymentModel model);
        public Task<PaymentModel> GetByIdAsync(PaymentModel Modelid);
        public Task<IEnumerable<PaymentModel>> GetAllAsync();
        public Task RemoveAsync(PaymentModel model);
    }
}
