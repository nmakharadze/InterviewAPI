using System.ComponentModel.DataAnnotations;

namespace Interview.Application.Persons.Queries.AdvancedSearch;

/// <summary>
/// დეტალური ძებნის ფილტრების DTO
/// მოიცავს ყველა ველის ძებნის კრიტერიუმებს
/// </summary>
public class AdvancedSearchFiltersDto
{
    /// <summary>
    /// ძებნის ინფორმაცია (სახელი, გვარი, პირადი ნომერი)
    /// </summary>
    public string? SearchTerm { get; set; }
    
    /// <summary>
    /// ფიზიკური პირის სახელი
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// ფიზიკური პირის გვარი
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// პირადი ნომერი
    /// </summary>
    public string? PersonalNumber { get; set; }
    
    /// <summary>
    /// დაბადების თარიღი (დაწყება)
    /// </summary>
    public DateTime? BirthDateFrom { get; set; }
    
    /// <summary>
    /// დაბადების თარიღი (დასრულება)
    /// </summary>
    public DateTime? BirthDateTo { get; set; }
    
    /// <summary>
    /// ქალაქის ID ფილტრისთვის
    /// </summary>
    public int? CityId { get; set; }
    
    /// <summary>
    /// სქესის ID ფილტრისთვის
    /// </summary>
    public int? GenderId { get; set; }
    
    /// <summary>
    /// აქვს თუ არა სურათი
    /// </summary>
    public bool? HasImage { get; set; }
    
    /// <summary>
    /// ტელეფონის ნომერი
    /// </summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>
    /// ტელეფონის ტიპის ID
    /// </summary>
    public int? PhoneTypeId { get; set; }
    
    /// <summary>
    /// ურთიერთობის ტიპის ID
    /// </summary>
    public int? RelationTypeId { get; set; }
    
    /// <summary>
    /// შექმნის თარიღი (დაწყება)
    /// </summary>
    public DateTime? CreatedFrom { get; set; }
    
    /// <summary>
    /// შექმნის თარიღი (დასრულება)
    /// </summary>
    public DateTime? CreatedTo { get; set; }
    
    /// <summary>
    /// განახლების თარიღი (დაწყება)
    /// </summary>
    public DateTime? UpdatedFrom { get; set; }
    
    /// <summary>
    /// განახლების თარიღი (დასრულება)
    /// </summary>
    public DateTime? UpdatedTo { get; set; }
    
    /// <summary>
    /// გვერდის ნომერი (1-დან იწყება)
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "გვერდის ნომერი უნდა იყოს 1-ზე მეტი")]
    public int Page { get; set; } = 1;
    
    /// <summary>
    /// გვერდზე ჩანაწერების რაოდენობა
    /// </summary>
    [Range(1, 100, ErrorMessage = "გვერდზე ჩანაწერების რაოდენობა უნდა იყოს 1-დან 100-მდე")]
    public int PageSize { get; set; } = 10;
}
