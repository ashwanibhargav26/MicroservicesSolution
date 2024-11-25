using AuthServiceApi.Application;
using AuthServiceApi.Application.Common;
using AuthServiceApi.Application.Repositories;
using AuthServiceApi.Infrastructure.Data;
using AuthServiceApi.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceApi.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, AppSettings configuration)
    {
        if (configuration.UseInMemoryDatabase)
        {
            services.AddDbContext<AuthServiceDbContext>(options =>
                options.UseInMemoryDatabase("HRMS"));
        }
        else
        {
            services.AddDbContext<AuthServiceDbContext>(options =>
                options.UseSqlServer(configuration.ConnectionStrings?.DefaultConnection));
        }

        // register services
        services.AddScoped<IUserRepository, UserRepository>();      
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<AuthServiceDbContextInitializer>();

        return services;
    }
}
