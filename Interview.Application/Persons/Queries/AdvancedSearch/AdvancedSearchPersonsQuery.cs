using MediatR;

namespace Interview.Application.Persons.Queries.AdvancedSearch;

/// <summary>
/// ფიზიკური პირების დეტალური ძებნის Query
/// მოიცავს ყველა ველის ძებნის კრიტერიუმებს
/// გამოიყენება CQRS პატერნში ფიზიკური პირების დეტალური ძებნისთვის
/// </summary>
public class AdvancedSearchPersonsQuery : IRequest<IEnumerable<AdvancedSearchResultDto>>
{
    /// <summary>
    /// ძებნის ფილტრები
    /// </summary>
    public AdvancedSearchFiltersDto Filters { get; set; } = new();
}
