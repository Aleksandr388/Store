using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Constants;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Providers.Interfaces;
using Store.DataAcess.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Providers
{
    public class TokensProvider : ITokenProvider
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokensProvider(UserManager<StoreUser> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Shared.Constants.DefaultValues.JwtKeyToken));
            _userManager = userManager;
        }

        public string CreateToken(StoreUser user, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(300),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string CreateRefreshToken(int length)
        {
            var random = new Random();
            var chars = Shared.Constants.DefaultValues.alphanumericCharacters;

            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }

        public async Task<TokenModel> CreateNewTokensAync(TokenModel tokenModel)
        {
            var handler = new JwtSecurityTokenHandler();

            var expiredJwtToken = handler.ReadToken(tokenModel.JwtToken) as JwtSecurityToken;
            var userEmail = expiredJwtToken.Claims.First(claim =>  claim.Type == "nameid").Value;

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
            {
                throw new CustomException(ErrorMessages.NoUsersWithThisId, HttpStatusCode.BadRequest);
            }
            if (user.RefreshToken != tokenModel.RefreshToken)
            {
                throw new CustomException(ErrorMessages.InvalidRefreshToken, HttpStatusCode.BadRequest);
            }

            var userRole = expiredJwtToken.Claims.First(claim => claim.Type == "role").Value;

            var newJwtToken = CreateToken(user, userRole);
            var newRefreshToken = CreateRefreshToken(32);

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                JwtToken = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }

    }
}



