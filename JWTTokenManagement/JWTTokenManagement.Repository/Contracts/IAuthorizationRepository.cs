using JWTTokenManagement.Repository.DBModels;
using System.Threading.Tasks;

namespace JWTTokenManagement.Repository.Contracts
{
    public interface IAuthorizationRepository
    {
        Task<bool> ValidateUser(User user);

        Task<int> GetUserID(string userName);

        Task<RefreshToken> SaveRefreshToken(RefreshToken refreshToken);

        Task<int> DeleteRefreshToken(string refreshToken, int UserID);
    }
}
