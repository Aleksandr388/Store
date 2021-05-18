using Microsoft.AspNetCore.Http;
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
            var checkEmail = await _userManager.FindByEmailAsync(userSignUpModel.Email);
            
            if (checkEmail is not null)
            {
                throw new CustomException(Shared.Constants.Errors.NoUsersWithThisEmail, StatusCodes.Status400BadRequest);
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
                throw new Exception(Shared.Constants.Errors.RegistrationFailed);
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, UserRole.Client.ToString().ToLower());

            if (!addToRoleResult.Succeeded)
            {
                throw new Exception(Shared.Constants.Errors.ErrorAddingRole);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = new UriBuilder(Shared.Constants.URLS.UrlConfirmEmail);
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["id"] = user.Id.ToString();
            parameters["code"] = code;
            callbackUrl.Query = parameters.ToString();
            Uri finalUrl = callbackUrl.Uri;

            await _emailProvider.SendEmailAsync(user.Email, Shared.Constants.Messages.ConfirmEmail,
            $"{Shared.Constants.Messages.ForConfirmedEmailGoToLink} <a href='{finalUrl}'>{Shared.Constants.Messages.RegistrationReference}</a>");

            return Shared.Constants.Messages.RegistrationDone;

        }
        public async Task<string> SignInAsync(UserSignInModel userSignInModel)
        {
            var checkEmail = await _userManager.FindByNameAsync(userSignInModel.Email);

            if (checkEmail is null)
            {
                throw new Exception(Shared.Constants.Errors.UserWithThisEmialNotRegiste);
            }

            var result = await _signInManager.PasswordSignInAsync(userSignInModel.Email, userSignInModel.Password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception(Shared.Constants.Errors.LoginFailedWrongPassword);
            }

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
                throw new Exception(Shared.Constants.Errors.NoUsersWithThisId);
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                throw new Exception(Shared.Constants.Errors.NoUsersWithThisEmail);
            }

            return Shared.Constants.Messages.EmailConfirmedSuccessfully;
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordUser forgotPasswordUser)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordUser.Email);
            
            if (user is null)
            {
                throw new Exception(Shared.Constants.Errors.NoUsersWithThisEmail);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var createPassword = _passwordGeneratorProvider.RandomPasswordGenerator(Shared.Constants.Values.PasswordValue);
            var result = await _userManager.ResetPasswordAsync(user, code, createPassword);

            if (!result.Succeeded)
            {
                throw new Exception(Shared.Constants.Errors.NoUsersWithThisEmail);
            }

            await _emailProvider.SendEmailAsync(user.Email, Shared.Constants.Messages.ResetPassword,
            $"{Shared.Constants.Messages.CreateNewPassword} {createPassword}");

            return Shared.Constants.Messages.PasswordSent;
        }
    }
}