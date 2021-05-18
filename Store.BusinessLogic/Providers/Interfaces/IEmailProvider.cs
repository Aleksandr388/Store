using System.Threading.Tasks;

namespace Store.BusinessLogic.Providers.Interfaces
{
    public interface IEmailProvider
    {
        public Task SendEmailAsync(string email, string subject, string messege);
    }
}
