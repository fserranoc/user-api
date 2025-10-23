using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Interfaces.Repository;
using Users.Infrastructure.Configuration;
using Users.Infrastructure.Data;
using Users.Infrastructure.Repositories;

namespace Users.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<DbOptions>(config.GetSection(nameof(DbOptions)));
            services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}