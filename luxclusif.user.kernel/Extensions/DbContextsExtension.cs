using luxclusif.user.application.Interfaces;
using luxclusif.user.domain.Repository;
using luxclusif.user.infrastructure;
using luxclusif.user.infrastructure.Repositories;
using luxclusif.user.infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace luxclusif.user.kernel.Extensions;
public static class DbContextsExtension
{
    public static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("PrincipalDatabase");


        if (!string.IsNullOrEmpty(conn))
        {
            services.AddDbContext<PrincipalContext>(
                options => options.UseNpgsql(conn));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        return services;
    }
}
