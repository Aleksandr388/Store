using Store.BusinessLogic.Models.Users;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<string> SignUpAsync(UserSignUpModel userSignUpModel);
        public Task<TokenModel> SignInAsync(UserSignInModel userSignInModel);
        public Task LogoutAsync();
        public Task<string> ConfirmEmailAsync(string id, string code);
        public Task<string> ForgotPasswordAsync(ForgotPasswordUser forgotPasswordUser);
    }
}