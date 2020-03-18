using JWTTokenManagement.Repository.Contracts;
using JWTTokenManagement.Repository.DBModels;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> GetUserID(string userName)
        {
            var userData = await _jWTHandlerContext.User.FirstOrDefaultAsync(x => (x.UserName == userName));
            if (userData != null)
            {
                return userData.UserId;
            }
            return 0;
        }

        public async Task<RefreshToken> SaveRefreshToken(RefreshToken refreshToken)
        {
            var refreshTokenData = await _jWTHandlerContext.RefreshToken.AddAsync(refreshToken);
            int recordsSaved =   await _jWTHandlerContext.SaveChangesAsync();
            return refreshToken;
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
