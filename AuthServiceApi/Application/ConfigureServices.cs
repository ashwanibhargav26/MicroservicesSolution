using AuthServiceApi.Application.Common.Interfaces;
using AuthServiceApi.Application.Common.Utilities;
using AuthServiceApi.Application.Services;
using AuthServiceApi.Web.Services;

namespace AuthServiceApi.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
       
        services.AddScoped<IMailService, MailService>();

        services.AddSingleton<ICurrentTime, CurrentTime>();
        services.AddSingleton<ICurrentUser, CurrentUser>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddSingleton<ICookieService, CookieService>();

        return services;
    }
}
