using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Providers.Interfaces;
using Store.DataAcess.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.BusinessLogic.Providers
{
    public class JwtProvider : IJwtProvider
    {

        private readonly SymmetricSecurityKey _key;

        public JwtProvider()
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Shared.Constants.Values.JwtKeyToken));
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

    }
}



