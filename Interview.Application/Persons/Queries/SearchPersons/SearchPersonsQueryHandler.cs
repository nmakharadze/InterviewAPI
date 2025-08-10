using Interview.Application.Persons.Queries.SearchPersons;
using Interview.Application.Persons.Queries;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Persons.Queries.SearchPersons;

/// <summary>
/// ფიზიკური პირების ძებნის Handler
/// მოიცავს სხვადასხვა კრიტერიუმებს ძებნისთვის
/// </summary>
public class SearchPersonsQueryHandler : IRequestHandler<SearchPersonsQuery, IEnumerable<SearchResultDto>>
{
    private readonly IPersonRepository _personRepository;

    public SearchPersonsQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IEnumerable<SearchResultDto>> Handle(SearchPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await _personRepository.SearchAsync(
            request.SearchTerm, 
            request.CityId, 
            request.GenderId, 
            request.Page, 
            request.PageSize);
        
        return persons.Select(p => new SearchResultDto
        {
            Id = p.Id,
            FirstName = p.FirstName.Value,
            LastName = p.LastName.Value,
            PersonalNumber = p.PersonalNumber.Value,
            City = p.City?.Name ?? string.Empty,
            CreatedAt = p.CreatedAt
        });
    }


}
