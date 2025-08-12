using System.Globalization;
using System.Threading;

namespace Interview.Api.Middleware;

/// <summary>
/// ლოკალიზაციის middleware - Accept-Language header-ის დამუშავება
/// </summary>
public class LocalizationMiddleware
{
    private readonly RequestDelegate _next;
    
    public LocalizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var acceptLanguage = context.Request.Headers["Accept-Language"].FirstOrDefault();
        var culture = GetCultureFromHeader(acceptLanguage);
        SetCulture(culture);
        
        await _next(context);
    }
    
    private string GetCultureFromHeader(string? acceptLanguage)
    {
        if (acceptLanguage?.Contains("en") == true) return "en-US";
        return "ka-GE";
    }
    
    private void SetCulture(string culture)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
    }
}
