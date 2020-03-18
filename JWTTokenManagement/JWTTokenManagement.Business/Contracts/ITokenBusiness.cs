using JWTTokenManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTTokenManagement.Business.Contracts
{
    public interface ITokenBusiness
    {
        Task<TokenModel> GenerateTokens(LoginModel loginModel);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
