using Interview.Application.DTOs.Person;
using Interview.Application.Queries.Person;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Queries.Person.Handlers;

/// <summary>
/// ერთი ფიზიკური პირის მიღების Handler
/// </summary>
public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonDto>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<PersonDto> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id);
        
        if (person == null)
            throw new ArgumentException($"ფიზიკური პირი ID {request.Id} ვერ მოიძებნა");
            
        return new PersonDto
        {
            Id = person.Id,
            FirstName = person.FirstName.Value,
            LastName = person.LastName.Value,
            PersonalNumber = person.PersonalNumber.Value,
            BirthDate = person.BirthDate.Value,
            Gender = person.Gender?.Name ?? string.Empty,
            City = person.City?.Name ?? string.Empty,
            ImagePath = person.ImagePath,
            CreatedAt = person.CreatedAt,
            UpdatedAt = person.UpdatedAt
        };
    }
}
