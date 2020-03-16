using JWTTokenManagement.Repository.Contracts;
using JWTTokenManagement.Repository.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JWTTokenManagement.Repository.Implementations
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly JWTTokenHandlerContext _jWTHandlerContext;
        public AuthorizationRepository(JWTTokenHandlerContext jWTHandlerContext)
        {
            _jWTHandlerContext = jWTHandlerContext;
        }

        public async Task<bool> ValidateUser(User user)
        {
            var userData = await _jWTHandlerContext.User.FirstOrDefaultAsync(x => (x.UserName == user.UserName && x.Password == user.Password));
            if (userData != null)
            {
                return true;
            }
            return false;
        }
    }
}
