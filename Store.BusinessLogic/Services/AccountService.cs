using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shared.Constants;
using Shared.Enums;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Providers.Interfaces;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using System;
using System.Net;
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
        private readonly IMapper _mapper;

        public AccountService(UserManager<StoreUser> userManager, SignInManager<StoreUser> signInManager,
            IEmailProvider emailProvider, IPasswordGeneratorProvider passwordGeneratorProvider, ITokenProvider jwtProvider, IMapper mapper)
        {
            _mapper = mapper;
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
                throw new CustomException(ErrorMessages.NoUsersWithThisEmail, HttpStatusCode.BadRequest);
            }

            var user = _mapper.Map<StoreUser>(userSignUpModel);

            var result = await _userManager.CreateAsync(user, userSignUpModel.Password);

            if (!result.Succeeded)
            {
                throw new CustomException(ErrorMessages.RegistrationFailed, HttpStatusCode.BadRequest);
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, UserRole.Client.ToString());

            if (!addToRoleResult.Succeeded)
            {
                throw new CustomException(ErrorMessages.ErrorAddingRole, HttpStatusCode.BadRequest);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = new UriBuilder(Shared.Constants.URLS.UrlConfirmEmail);
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters[DefaultValues.Id] = user.Id.ToString();
            parameters[DefaultValues.Code] = code;
            callbackUrl.Query = parameters.ToString();
            Uri finalUrl = callbackUrl.Uri;

            await _emailProvider.SendEmailAsync(user.Email, Messages.ConfirmEmail,
            $"{Messages.ForConfirmedEmailGoToLink} <a href='{finalUrl}'>{Messages.RegistrationReference}</a>");

            return Messages.RegistrationDone;
        }
        public async Task<TokenModel> SignInAsync(UserSignInModel userSignInModel)
        {
            var singInUser = await _userManager.FindByNameAsync(userSignInModel.Email);

            if (singInUser is null)
            {
                throw new CustomException(ErrorMessages.UserWithThisEmialNotRegiste, HttpStatusCode.BadRequest);
            }

            var result = await _signInManager.PasswordSignInAsync(userSignInModel.Email, userSignInModel.Password, false, false);

            if (!result.Succeeded)
            {
                throw new CustomException(ErrorMessages.LoginFailedWrongPassword, HttpStatusCode.BadRequest);
            }

            var jwtUserToken = _jwtProvider.CreateToken(singInUser, UserRole.Client.ToString());

            var refreshToken = _jwtProvider.CreateRefreshToken(32);
            singInUser.RefreshToken = refreshToken;

            await _userManager.UpdateAsync(singInUser);

            var roles = await _userManager.GetRolesAsync(singInUser);

            if (roles.Contains(UserRole.Admin.ToString()))
            {
                var jwtAdminToken = _jwtProvider.CreateToken(singInUser, UserRole.Admin.ToString());
                var createJwtTokenForAdmin = new TokenModel
                {
                    RefreshToken = refreshToken,
                    JwtToken = jwtAdminToken
                };

                return createJwtTokenForAdmin;
            }

            var tokenClientModels = new TokenModel
            {
                RefreshToken = refreshToken,
                JwtToken = jwtUserToken
            };

            return tokenClientModels;
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
                throw new Exception(ErrorMessages.NoUsersWithThisId);
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                throw new Exception(ErrorMessages.NoUsersWithThisEmail);
            }

            return Messages.EmailConfirmedSuccessfully;
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordUser forgotPasswordUser)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordUser.Email);

            if (user is null)
            {
                throw new CustomException(ErrorMessages.NoUsersWithThisEmail, HttpStatusCode.BadRequest);
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var newPassword = _passwordGeneratorProvider.RandomPasswordGenerator(DefaultValues.PasswordValue);
            var result = await _userManager.ResetPasswordAsync(user, code, newPassword);

            if (!result.Succeeded)
            {
                throw new CustomException(ErrorMessages.NoUsersWithThisEmail, HttpStatusCode.BadRequest);
            }

            await _emailProvider.SendEmailAsync(user.Email, Messages.ResetPassword,
            $"{Messages.CreateNewPassword} {newPassword}");

            return Messages.PasswordSent;
        }
    }
}