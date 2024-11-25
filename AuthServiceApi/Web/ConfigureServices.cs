using System.Diagnostics;
using System.Reflection;
using AuthServiceApi.Application.Common;
using AuthServiceApi.Web.Extensions;
using AuthServiceApi.Web.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace AuthServiceApi.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebAPIService(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddAuthentication();
        services.AddAuthorization();

        // Middleware
        services.AddSingleton<GlobalExceptionMiddleware>();
        services.AddSingleton<PerformanceMiddleware>();
        services.AddSingleton<Stopwatch>();

        // Extension classes
        services.AddHealthChecks();
        services.AddCompressionCustom();
        services.AddCorsCustom(appSettings);
        services.AddHttpClient();
        services.AddSwaggerOpenAPI(appSettings);
        services.AddJWTCustom(appSettings);
        services.SetupHealthCheck(appSettings);

        return services;
    }
}
