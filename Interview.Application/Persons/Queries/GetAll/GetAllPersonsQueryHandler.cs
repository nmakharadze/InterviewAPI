using Interview.Application.Persons.Queries.GetAll;
using Interview.Application.Persons.Queries;
using Interview.Application.Repositories;
using Interview.Application.Repositories.Person;
using MediatR;

namespace Interview.Application.Persons.Queries.GetAll;

/// <summary>
/// ყველა ფიზიკური პირის სიის მიღების Handler
/// მოიცავს paging ფუნქციონალს
/// </summary>
public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonListDto>>
{
    private readonly IPersonRepository _personRepository;

    public GetAllPersonsQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<IEnumerable<PersonListDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await _personRepository.GetAllAsync(request.Page, request.PageSize);
        
        return persons.Select(p => new PersonListDto
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
