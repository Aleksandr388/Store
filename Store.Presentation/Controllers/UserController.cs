using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetByIdUser")]
        public async Task<UserModel> GetByIdUser([FromQuery]string token)
        {
            return await _userService.GetByIdUserAsync(token);
        }

        [HttpPost("UpdateUser")]
        public async Task UpdateUser(UpdateUserModel updateUserModel)
        {
            await _userService.UpdateUserAsync(updateUserModel);
        }
    }
}
