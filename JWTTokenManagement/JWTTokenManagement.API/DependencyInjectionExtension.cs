using JWTTokenManagement.Business;
using JWTTokenManagement.Business.Contracts;
using JWTTokenManagement.Business.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JWTTokenManagement.API
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterAPIDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterBusinessDependencies(configuration);
            services.AddTransient<IAuthorizationBusiness, AuthorizationBusiness>();
            return services;
        }
    }
}
