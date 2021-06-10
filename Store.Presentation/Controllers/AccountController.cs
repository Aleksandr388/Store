using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace Store.Presentation.Controllers
{
    [Route("api/{controller}")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody]UserSignUpModel userSignUpModel)
        {
            var result = await _accountService.SignUpAsync(userSignUpModel);

            return Ok(result);
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]UserSignInModel userSignInModel)
        {
            var result = await _accountService.SignInAsync(userSignInModel);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string id, string code)
        {
            var result = await _accountService.ConfirmEmailAsync(id, code);

            return Ok(result);
        }
        [Authorize/*(AuthenticationSchemes = "Bearer", Roles = "Client")*/]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordUser forgotPasswordUser)
        {
            var result = await _accountService.ForgotPasswordAsync(forgotPasswordUser);

            return Ok(result);
        }
        //public async Task<IActionResult> Logout()
        //{
        //   return Ok(await _accountService.LogoutAsync());
        //}
    }
}
