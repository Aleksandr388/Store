using Store.BusinessLogic.Models.Payments;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task CreatePaymentAsync(PayModel model, long userId);
    }
}
