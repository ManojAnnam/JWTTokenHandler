using JWTTokenManagement.Repository.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWTTokenManagement.Repository.Contracts
{
    public interface IAuthorizationRepository
    {
        Task<bool> ValidateUser(User user);
    }
}
