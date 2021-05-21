using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenProvider _tokenProvider;
        public TokenController(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }
        [HttpPost("CreateNewToken")]
        public async Task<IActionResult> CreateNewToken([FromBody] TokenModel tokenModel)
        {
            var result = await _tokenProvider.CreateNewTokensAync(tokenModel);

            return Ok(result);
        }
    }
}
