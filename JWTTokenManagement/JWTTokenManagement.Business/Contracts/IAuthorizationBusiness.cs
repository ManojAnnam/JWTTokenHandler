using JWTTokenManagement.Models.Models;
using System.Threading.Tasks;

namespace JWTTokenManagement.Business.Contracts
{
    public interface IAuthorizationBusiness
    {
        Task<bool> ValidateUser(LoginModel loginModel);
    }
}
