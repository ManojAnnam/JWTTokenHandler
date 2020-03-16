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
        public AuthorizationBusiness(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public async Task<bool> ValidateUser(LoginModel loginModel)
        {
            User user = Mappers.AuthorizationMapper.MapDomainUserModelToRepositoryUserModel(loginModel);
            bool isValidUser = await _authorizationRepository.ValidateUser(user);
            return isValidUser;
        }
    }
}
