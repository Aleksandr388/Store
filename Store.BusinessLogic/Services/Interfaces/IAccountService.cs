using Store.BusinessLogic.Models.Users;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<string> SignUpAsync(UserSignUpModel userSignUpModel);
    }
}