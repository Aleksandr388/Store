using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Entities.Enums;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly SignInManager<StoreUser> _signInManager;

        public AccountService(UserManager<StoreUser> userManager, SignInManager<StoreUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> SignUpAsync(UserSignUpModel userSignUpModel)
        {
            StoreUser user = new StoreUser
            {
                Email = userSignUpModel.Email,
                UserName = userSignUpModel.Email,
                FirstName = userSignUpModel.FirsName,
                LastName = userSignUpModel.LastName,
            };

            var resul = await _userManager.CreateAsync(user, userSignUpModel.Password);
            if (resul.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRole.Client.ToString().ToLower());
                await _signInManager.SignInAsync(user, false);
            }
            

            return resul.ToString();
        }
        public async Task<string> SignInAsync(UserSignInModel userSignInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(userSignInModel.Email, userSignInModel.Password, false,  false);

            return result.ToString();
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
