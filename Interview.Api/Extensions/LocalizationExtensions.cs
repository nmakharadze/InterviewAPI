using Interview.Application.Services;
using Interview.Api.Services;
using Interview.Api.Localization.Resources;
using Microsoft.Extensions.Localization;

namespace Interview.Api.Extensions;

/// <summary>
/// ლოკალიზაციის extensions კლასი
/// </summary>
public static class LocalizationExtensions
{
    public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Localization/Resources");
        services.AddScoped<ILocalizationService, LocalizationService>();
        return services;
    }
    
    public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<Middleware.LocalizationMiddleware>();
    }
}
