namespace Interview.Application.Services;

/// <summary>
/// ლოკალიზაციის სერვისის ინტერფეისი
/// </summary>
public interface ILocalizationService
{
    /// <summary>
    /// ლოკალიზებული მესიჯის მიღება გასაღების მიხედვით
    /// </summary>
    /// <param name="key">მესიჯის გასაღები</param>
    /// <returns>ლოკალიზებული მესიჯი</returns>
    string GetLocalizedString(string key);

    /// <summary>
    /// მიმდინარე კულტურის მიღება
    /// </summary>
    /// <returns>მიმდინარე კულტურის სახელი</returns>
    string GetCurrentCulture();
}
