using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<StoreUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserModel> GetByIdUserAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var userToken = tokenHandler.ReadJwtToken(token);
            var userId = userToken.Claims.First(claim => claim.Type == "nameid").Value;

            if (userId is null)
            {
                throw new Exception();
            }

            var user = await _userManager.FindByIdAsync(userId);

            var userModel = _mapper.Map<UserModel>(user);

            return userModel;
        }

        public Task SaveChagesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(UpdateUserModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var userToken = tokenHandler.ReadJwtToken(model.Jwt);
            var userId = userToken.Claims.First(claim => claim.Type == "nameid").Value;

            var checkUser = await _userManager.FindByIdAsync(userId);

            if (checkUser is null)
            {
                throw new Exception();
            }
            checkUser.FirstName = model.FirstName;
            checkUser.LastName = model.LastName;


            await  _userManager.SetUserNameAsync(checkUser, model.Email);
            await _userManager.SetEmailAsync(checkUser, model.Email);

            if (model.CurrentPassword is not null || model.NewPassword is not null)
            {
                await _userManager.ChangePasswordAsync(checkUser, model.CurrentPassword, model.NewPassword);

            }

            await _userManager.UpdateAsync(checkUser);
        }
    }
}
