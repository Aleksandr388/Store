using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Providers.Interfaces;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Entities.Enums;
using System;
using System.Threading.Tasks;
using System.Web;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly IEmailProvider _emailProvider;
        private readonly IPasswordGeneratorProvider _passwordGeneratorProvider;

        public AccountService(UserManager<StoreUser> userManager, SignInManager<StoreUser> signInManager, IEmailProvider emailProvider, IPasswordGeneratorProvider passwordGeneratorProvider)
        {
            _userManager = userManager;
            _emailProvider = emailProvider;
            _signInManager = signInManager;
            _passwordGeneratorProvider = passwordGeneratorProvider;
        }

        public async Task<string> SignUpAsync(UserSignUpModel userSignUpModel)
        {
            var EmailCheck = await _userManager.FindByEmailAsync(userSignUpModel.Email);
            if (EmailCheck != null)
            {
                throw new Exception();
            }

            StoreUser user = new StoreUser
            {
                UserName = userSignUpModel.Email,
                Email = userSignUpModel.Email,
                FirstName = userSignUpModel.FirstName,
                LastName = userSignUpModel.LastName
            };

            var result = await _userManager.CreateAsync(user, userSignUpModel.Password);

            if (!result.Succeeded)
            {
                throw new Exception();
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, UserRole.Client.ToString().ToLower());

            if (!addToRoleResult.Succeeded)
            {
                throw new Exception();
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = new UriBuilder("https://localhost:5001/api/Account/ConfirmEmail");
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["id"] = user.Id.ToString();
            parameters["code"] = code;
            callbackUrl.Query = parameters.ToString();
            Uri finalUrl = callbackUrl.Uri;

            await _emailProvider.SendEmailAsync(user.Email, "Confirm email",
            $"For confirmed Email go to link : <a href='{finalUrl}'>registration reference</a>");

            return "registration done";

        }
        public async Task<string> SignInAsync(UserSignInModel userSignInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(userSignInModel.Email, userSignInModel.Password, false, false);

            return result.ToString();
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> ConfirmEmailAsync(string id, string code)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                throw new Exception();
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                throw new Exception();
            }

            return "ok";
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordUser forgotPasswordUser)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordUser.Email);
            
            if (user is null)
            {
                throw new CustomExeption();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var createPassword = _passwordGeneratorProvider.RandomPasswordGenerator(8);
            var result = await _userManager.ResetPasswordAsync(user, code, createPassword);

            if (!result.Succeeded)
            {
                throw new CustomExeption();
            }

            await _emailProvider.SendEmailAsync(user.Email, "reset password",
            $"we create new password is: {createPassword}");

            return "Password sent";
        }
    }
}