using Interview.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Api.Controllers;

/// <summary>
/// ლოკალიზაციის ტესტინგის კონტროლერი
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LocalizationTestController : ControllerBase
{
    private readonly ILocalizationService _localizationService;

    public LocalizationTestController(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }

    /// <summary>
    /// მიმდინარე კულტურის მიღება
    /// </summary>
    [HttpGet("culture")]
    public IActionResult GetCurrentCulture()
    {
        var culture = _localizationService.GetCurrentCulture();
        var message = _localizationService.GetLocalizedString("CurrentCulture");
        
        return Ok(new
        {
            culture = culture,
            message = string.Format(message, culture)
        });
    }

    /// <summary>
    /// ლოკალიზებული მესიჯის ტესტინგი
    /// </summary>
    [HttpGet("message/{key}")]
    public IActionResult GetLocalizedMessage(string key)
    {
        var message = _localizationService.GetLocalizedString(key);
        
        return Ok(new
        {
            key = key,
            message = message,
            culture = _localizationService.GetCurrentCulture()
        });
    }

    /// <summary>
    /// შეცდომის ტესტინგი
    /// </summary>
    [HttpGet("error-test")]
    public IActionResult TestError()
    {
        throw new ArgumentException("This is a test error");
    }
}
