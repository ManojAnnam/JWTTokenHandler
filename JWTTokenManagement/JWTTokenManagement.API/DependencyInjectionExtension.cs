using JWTTokenManagement.Business;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTokenManagement.API
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterAPIDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterBusinessDependencies(configuration);
            //services.AddTransient<IAuthorizationBusiness, AuthorizationBusiness>();
            return services;
        }
    }
}
