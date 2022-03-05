using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Infrastructure.Services
{
    public class AuthenticationServices
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        public AuthenticationServices(
            IConfiguration configuration,
            JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _configuration = configuration;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }


        public string GenerateAuthenticationToken(UserManagement.Core.Enums.UserTypes userType)
        {
            List<Claim> claims = new List<Claim>();

            if (userType != Core.Enums.UserTypes.None)
                claims.Add(new Claim("role", userType.ToString()));

            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JwtIssuerSettings:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);



            return _jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
