using Interview.Application.Services;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Interview.Infrastructure.Services;

/// <summary>
/// ლოკალიზაციის სერვისის იმპლემენტაცია
/// </summary>
public class LocalizationService : ILocalizationService
{
    private readonly ResourceManager _resourceManager;

    public LocalizationService()
    {
        _resourceManager = new ResourceManager(
            "Interview.Infrastructure.Localization.Resources.SharedResource",
            typeof(LocalizationService).Assembly
        );
    }

    public string GetLocalizedString(string key)
    {
        return _resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? key;
    }

    public string GetCurrentCulture()
    {
        return Thread.CurrentThread.CurrentCulture.Name;
    }
}
