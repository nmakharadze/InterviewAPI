using MediatR;
using Interview.Application.Repositories;

namespace Interview.Application.Persons.Queries.AdvancedSearch;

/// <summary>
/// ფიზიკური პირების დეტალური ძებნის Query Handler
/// მოიცავს ბიზნეს ლოგიკას დეტალური ძებნისთვის
/// </summary>
public class AdvancedSearchPersonsQueryHandler : IRequestHandler<AdvancedSearchPersonsQuery, IEnumerable<AdvancedSearchResultDto>>
{
    private readonly IPersonRepository _personRepository;

    public AdvancedSearchPersonsQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IEnumerable<AdvancedSearchResultDto>> Handle(AdvancedSearchPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await _personRepository.AdvancedSearchAsync(request.Filters);
        
        return persons.Select(p => new AdvancedSearchResultDto
        {
            Id = p.Id,
            FirstName = p.FirstName.Value,
            LastName = p.LastName.Value,
            PersonalNumber = p.PersonalNumber.Value,
            BirthDate = p.BirthDate.Value,
            Gender = p.Gender?.Name ?? string.Empty,
            City = p.City?.Name ?? string.Empty,
            ImagePath = p.ImagePath,
            HasImage = !string.IsNullOrEmpty(p.ImagePath),
            PhoneNumbersCount = p.PhoneNumbers?.Count ?? 0,
            RelationsCount = p.Relations?.Count ?? 0,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        });
    }
}
