namespace Interview.Application.Persons.Queries.AdvancedSearch;

/// <summary>
/// დეტალური ძებნის შედეგების DTO
/// მოიცავს ფიზიკური პირის სრულ ინფორმაციას
/// </summary>
public class AdvancedSearchResultDto
{
    /// <summary>
    /// ფიზიკური პირის ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ფიზიკური პირის სახელი
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// ფიზიკური პირის გვარი
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// პირადი ნომერი
    /// </summary>
    public string PersonalNumber { get; set; } = string.Empty;

    /// <summary>
    /// დაბადების თარიღი
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// სქესი
    /// </summary>
    public string Gender { get; set; } = string.Empty;

    /// <summary>
    /// ქალაქი
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// სურათის მისამართი
    /// </summary>
    public string? ImagePath { get; set; }

    /// <summary>
    /// აქვს თუ არა სურათი
    /// </summary>
    public bool HasImage { get; set; }

    /// <summary>
    /// ტელეფონის ნომრების რაოდენობა
    /// </summary>
    public int PhoneNumbersCount { get; set; }

    /// <summary>
    /// ურთიერთობების რაოდენობა
    /// </summary>
    public int RelationsCount { get; set; }

    /// <summary>
    /// შექმნის თარიღი
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// განახლების თარიღი
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
