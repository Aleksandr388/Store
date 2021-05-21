using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Enums;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Providers.Interfaces;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
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
        private readonly ITokenProvider _jwtProvider;

        public AccountService(UserManager<StoreUser> userManager, SignInManager<StoreUser> signInManager, IEmailProvider emailProvider, IPasswordGeneratorProvider passwordGeneratorProvider, ITokenProvider jwtProvider)
        {
            _userManager = userManager;
            _emailProvider = emailProvider;
            _signInManager = signInManager;
            _passwordGeneratorProvider = passwordGeneratorProvider;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> SignUpAsync(UserSignUpModel userSignUpModel)
        {
            var findByEmail = await _userManager.FindByEmailAsync(userSignUpModel.Email);
            
            if (findByEmail is not null)
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
                throw new CustomException(Shared.Constants.Errors.RegistrationFailed, StatusCodes.Status400BadRequest);
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, UserRole.Client.ToString().ToLower());

            if (!addToRoleResult.Succeeded)
            {
                throw new CustomException(Shared.Constants.Errors.ErrorAddingRole, StatusCodes.Status400BadRequest);
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
        public async Task<TokenModel> SignInAsync(UserSignInModel userSignInModel)
        {
            var singInUser = await _userManager.FindByNameAsync(userSignInModel.Email);

            if (singInUser is null)
            {
                throw new CustomException(Shared.Constants.Errors.UserWithThisEmialNotRegiste, StatusCodes.Status400BadRequest);
            }

            var result = await _signInManager.PasswordSignInAsync(userSignInModel.Email, userSignInModel.Password, false, false);

            if (!result.Succeeded)
            {
                throw new CustomException(Shared.Constants.Errors.LoginFailedWrongPassword, StatusCodes.Status400BadRequest);
            }

            var refreshToken = _jwtProvider.CreateRefreshToken(32);
            singInUser.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(singInUser);

            var roles = await _userManager.GetRolesAsync(singInUser);
            if (roles.Contains(UserRole.Admin.ToString()))
            {
                var jwtAdminToken = _jwtProvider.CreateToken(singInUser, UserRole.Admin.ToString());

                var tokenAdminrModels = new TokenModel
                {
                    RefreshToken = refreshToken,
                    JwtToken = jwtAdminToken
                };

                return tokenAdminrModels;
            }

            var jwtUserToken = _jwtProvider.CreateToken(singInUser, UserRole.Client.ToString());
            
            var tokenUserModels = new TokenModel
            {
                RefreshToken = refreshToken,
                JwtToken = jwtUserToken
            };

            return tokenUserModels;
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
                throw new CustomException(Shared.Constants.Errors.NoUsersWithThisEmail, StatusCodes.Status400BadRequest);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var newPassword = _passwordGeneratorProvider.RandomPasswordGenerator(Shared.Constants.Values.PasswordValue);
            var result = await _userManager.ResetPasswordAsync(user, code, newPassword);

            if (!result.Succeeded)
            {
                throw new CustomException(Shared.Constants.Errors.NoUsersWithThisEmail, StatusCodes.Status400BadRequest);
            }

            await _emailProvider.SendEmailAsync(user.Email, Shared.Constants.Messages.ResetPassword,
            $"{Shared.Constants.Messages.CreateNewPassword} {newPassword}");

            return Shared.Constants.Messages.PasswordSent;
        }
    }
}