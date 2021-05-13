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
        //public async Task<IActionResult> Logout()
        //{
        //   return Ok(await _accountService.LogoutAsync());
        //}
    }
}
