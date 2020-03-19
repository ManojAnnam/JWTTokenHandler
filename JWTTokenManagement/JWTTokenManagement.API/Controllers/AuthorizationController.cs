using JWTTokenManagement.Business.Contracts;
using JWTTokenManagement.Models.Constants;
using JWTTokenManagement.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JWTTokenManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationBusiness _authorizationBusiness;
        public AuthorizationController(IAuthorizationBusiness authorizationBusiness)
        {
            _authorizationBusiness = authorizationBusiness;
        }

        [Route(Constants.ValidateLogin)]
        [HttpPost]
        public async Task<IActionResult> ValidateLogin(LoginModel loginModel)
        {
            try
            {
                var isValidUser = await _authorizationBusiness.ValidateUser(loginModel);
                if (isValidUser)
                {
                    return Ok(isValidUser);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [Route(Constants.RefreshToken)]
        [HttpPost]
        public async Task<IActionResult> RefreshTokens(TokenModel tokenModel)
        {
            try
            {
                var result = await _authorizationBusiness.Refresh(tokenModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}