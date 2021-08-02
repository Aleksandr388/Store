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
        public async Task SignUp([FromBody]UserSignUpModel userSignUpModel)
        {
             await _accountService.SignUpAsync(userSignUpModel);
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
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordUser forgotPasswordUser)
        {
            var result = await _accountService.ForgotPasswordAsync(forgotPasswordUser);

            return Ok(result);
        }
    }
}
