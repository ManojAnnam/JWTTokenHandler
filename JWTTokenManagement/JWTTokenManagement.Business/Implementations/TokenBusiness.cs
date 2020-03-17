using JWTTokenManagement.Business.Contracts;
using JWTTokenManagement.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTTokenManagement.Business.Implementations
{
    public class TokenBusiness : ITokenBusiness
    {
        private readonly IConfiguration _configuration;
        public TokenBusiness(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenModel GenerateTokens(LoginModel loginModel)
        {
            var token = GenerateAccessToken(new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, loginModel.UserName),
                    new Claim(ClaimTypes.Role, "Admin")
                });

           // var refreshToken = GenerateRefreshToken();
            //SaveRefreshToken(username, deviceId, refreshToken);
            return new TokenModel { AccessToken = token, RefreshToken = "dsfdsfdsfdsfd" };
        }

        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"])),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
    }
}
