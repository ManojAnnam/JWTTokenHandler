using JWTTokenManagement.Business.Contracts;
using JWTTokenManagement.Models.Constants;
using JWTTokenManagement.Models.Models;
using JWTTokenManagement.Repository.Contracts;
using JWTTokenManagement.Repository.DBModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JWTTokenManagement.Business.Implementations
{
    public class TokenBusiness : ITokenBusiness
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthorizationRepository _authorizationRepository;
        public TokenBusiness(IConfiguration configuration , IAuthorizationRepository authorizationRepository)
        {
            _configuration = configuration;
            _authorizationRepository = authorizationRepository;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Issuer"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException(Constants.InvalidToken);

            return principal;
        }

        public async Task<TokenModel> GenerateTokens(LoginModel loginModel)
        {
            var token = GenerateAccessToken(new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, loginModel.UserName),
                    new Claim(ClaimTypes.Role, "Admin")
                });

            var refreshToken = GenerateRefreshToken();
            await SaveRefreshToken(loginModel.UserName, refreshToken);
            return new TokenModel { AccessToken = token, RefreshToken = refreshToken };
        }

        private async Task SaveRefreshToken(string username, string refreshToken)
        {
            int userID = await _authorizationRepository.GetUserID(username);
            if(userID > 0)
            {
                RefreshToken refreshTokenObj = new RefreshToken
                {
                    Token = refreshToken,
                    UserId = userID
                };
                var addedToken =  await _authorizationRepository.SaveRefreshToken(refreshTokenObj);
            }
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

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }


    }
}
