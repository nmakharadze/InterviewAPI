using Interview.Application.Services;
using Interview.Api.Localization.Resources;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading;

namespace Interview.Api.Services;

/// <summary>
/// ლოკალიზაციის სერვისის იმპლემენტაცია
/// </summary>
public class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    
    public LocalizationService(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;
    }
    
    public string GetLocalizedString(string key)
    {
        return _localizer[key];
    }
    
    public string GetCurrentCulture()
    {
        return Thread.CurrentThread.CurrentCulture.Name;
    }
}

