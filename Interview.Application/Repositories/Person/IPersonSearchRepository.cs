using Interview.Domain.Entities.Person;
using Interview.Application.Persons.Queries.AdvancedSearch;
using PersonEntity = Interview.Domain.Entities.Person.Person;

namespace Interview.Application.Repositories.Person;

/// <summary>
/// ფიზიკური პირების ძებნის Repository ინტერფეისი
/// მოიცავს ძებნის ოპერაციებს
/// </summary>
public interface IPersonSearchRepository
{
    /// <summary>
    /// მარტივი ძებნა ფიზიკური პირებისთვის
    /// </summary>
    Task<IEnumerable<PersonEntity>> SearchAsync(string? searchTerm, int? cityId, int? genderId, int page, int pageSize);
    
    /// <summary>
    /// დეტალური ძებნა ფიზიკური პირებისთვის
    /// </summary>
    Task<IEnumerable<PersonEntity>> AdvancedSearchAsync(AdvancedSearchFiltersDto filters);
}
