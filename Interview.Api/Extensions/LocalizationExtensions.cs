using Interview.Application.Services;
using Interview.Infrastructure.Services;

namespace Interview.Api.Extensions;

/// <summary>
/// ლოკალიზაციის extensions კლასი
/// </summary>
public static class LocalizationExtensions
{
    public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
    {
        services.AddScoped<ILocalizationService, LocalizationService>();
        return services;
    }
    
    public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<Middleware.LocalizationMiddleware>();
    }
}
