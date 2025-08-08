using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Interview.Api.Middleware;

/// <summary>
/// Global exception handling middleware
/// გამოიყენება ყველა exception-ის დამუშავებისთვის
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new
        {
            error = exception.Message,
            details = _environment.IsDevelopment() ? exception.StackTrace : null,
            timestamp = DateTime.UtcNow
        };

        switch (exception)
        {
            case ArgumentException:
                response.StatusCode = 400; // Bad Request
                break;
            case InvalidOperationException:
                response.StatusCode = 400; // Bad Request
                break;
            case ValidationException:
                response.StatusCode = 400; // Bad Request
                break;
            default:
                response.StatusCode = 500; // Internal Server Error
                break;
        }

        await response.WriteAsJsonAsync(errorResponse);
    }
}

/// <summary>
/// Extension method middleware- ის რეგისტრაციისთვის
/// </summary>
public static class GlobalExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
