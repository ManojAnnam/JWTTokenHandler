using JWTTokenManagement.Models.Models;
using JWTTokenManagement.Repository.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWTTokenManagement.Business.Mappers
{
    public static class AuthorizationMapper
    {
        public static User MapDomainUserModelToRepositoryUserModel(LoginModel loginModel)
        {
            User user = new User()
            {
                UserName = loginModel.UserName,
                Password = loginModel.Password
            };
            return user;
        }
    }
}
