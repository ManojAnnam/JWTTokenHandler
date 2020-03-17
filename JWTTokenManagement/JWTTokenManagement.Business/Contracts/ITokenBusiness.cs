using JWTTokenManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWTTokenManagement.Business.Contracts
{
    public interface ITokenBusiness
    {
        TokenModel GenerateTokens(LoginModel loginModel);
    }
}
