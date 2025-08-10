using Interview.Application.Persons.Queries.SearchPersons;
using MediatR;

namespace Interview.Application.Persons.Queries.SearchPersons;

/// <summary>
/// ფიზიკური პირების ძებნის Query
/// მოიცავს სხვადასხვა კრიტერიუმებს ძებნისთვის
/// გამოიყენება CQRS პატერნში ფიზიკური პირების ძებნისთვის
/// </summary>
public class SearchPersonsQuery : IRequest<IEnumerable<SearchResultDto>>
{
    /// <summary>
    /// ძებნის ინფორმაცია (სახელი, გვარი, პირადი ნომერი)
    /// </summary>
    public string? SearchTerm { get; set; }
    
    /// <summary>
    /// ქალაქის ID ფილტრისთვის
    /// </summary>
    public int? CityId { get; set; }
    
    /// <summary>
    /// სქესის ID ფილტრისთვის
    /// </summary>
    public int? GenderId { get; set; }
    
    /// <summary>
    /// გვერდის ნომერი (1-დან იწყება)
    /// </summary>
    public int Page { get; set; } = 1;
    
    /// <summary>
    /// გვერდზე ჩანაწერების რაოდენობა
    /// </summary>
    public int PageSize { get; set; } = 10;
}

