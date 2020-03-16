using JWTTokenManagement.Repository;
using JWTTokenManagement.Repository.Contracts;
using JWTTokenManagement.Repository.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JWTTokenManagement.Business
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterBusinessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterRepositoryDependencies(configuration);
            services.AddTransient<IAuthorizationRepository, AuthorizationRepository>();
            return services;
        }
    }
}
