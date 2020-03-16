using JWTTokenManagement.Repository.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWTTokenManagement.Repository
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterRepositoryDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JWTTokenHandlerContext>(o => o.UseSqlServer(configuration["connectionStrings:JWTTokenHandlerDBConnectionString"]));
            return services;
        }
    }
}
