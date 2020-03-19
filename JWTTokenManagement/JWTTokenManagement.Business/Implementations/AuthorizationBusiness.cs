using JWTTokenManagement.Business.Contracts;
using JWTTokenManagement.Models.Models;
using JWTTokenManagement.Repository.Contracts;
using JWTTokenManagement.Repository.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWTTokenManagement.Business.Implementations
{
    public class AuthorizationBusiness :  IAuthorizationBusiness
    {
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly ITokenBusiness _tokenBusiness;
        public AuthorizationBusiness(ITokenBusiness tokenBusiness,IAuthorizationRepository authorizationRepository)
        {
            _tokenBusiness = tokenBusiness;
            _authorizationRepository = authorizationRepository;
        }

        public Task<TokenModel> Refresh(TokenModel refreshTokenParams)
        {
            var principal = _tokenBusiness.GetPrincipalFromExpiredToken(refreshTokenParams.AccessToken);
            var username = principal.Identity.Name;          
            //var savedRefreshToken = _userDataAccess.GetRefreshToken(username, deviceClaim.Value);
            //if (savedRefreshToken != refreshTokenParams.RefreshToken)
            //    throw new SecurityTokenException(InvalidRefreshTokenException);

            //var newTokens = _tokenService.GenerateTokens(principal.Claims);
            //_userDataAccess.SaveRefreshToken(username, deviceClaim.Value, newTokens.RefreshToken);
            return null;
        }


        public async Task<bool> ValidateUser(LoginModel loginModel)
        {
            User user = Mappers.AuthorizationMapper.MapDomainUserModelToRepositoryUserModel(loginModel);

            bool isValidUser = await _authorizationRepository.ValidateUser(user);
            if (isValidUser)
            {
                var tokens =await _tokenBusiness.GenerateTokens(loginModel);
            }
            return isValidUser;
        }
    }
}
