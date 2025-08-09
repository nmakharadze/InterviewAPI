using Interview.Application.DTOs.Person;
using Interview.Application.Queries.Person;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Queries.Person.Handlers;

/// <summary>
/// ფიზიკური პირის სრული ინფორმაციის მიღების Handler
/// მოიცავს ტელეფონის ნომრებს და კავშირებს
/// </summary>
public class GetPersonDetailQueryHandler : IRequestHandler<GetPersonDetailQuery, PersonDetailDto>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonDetailQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<PersonDetailDto> Handle(GetPersonDetailQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdWithDetailsAsync(request.Id);
        
        if (person == null)
            throw new ArgumentException($"ფიზიკური პირი ID {request.Id} ვერ მოიძებნა");
            
        return new PersonDetailDto
        {
            Id = person.Id,
            FirstName = person.FirstName.Value,
            LastName = person.LastName.Value,
            PersonalNumber = person.PersonalNumber.Value,
            BirthDate = person.BirthDate.Value,
            Gender = person.Gender?.Name ?? string.Empty,
            City = person.City?.Name ?? string.Empty,
            ImagePath = person.ImagePath,
            PhoneNumbers = person.PhoneNumbers?.Select(pn => new PhoneNumberDto
            {
                Id = pn.Id,
                Number = pn.Number.Value,
                PhoneType = pn.PhoneType?.Name ?? string.Empty
            }).ToList() ?? new List<PhoneNumberDto>(),
            Relations = person.Relations?.Select(r => new RelationDto
            {
                Id = r.Id,
                RelatedPersonId = r.RelatedPersonId,
                RelatedPersonName = $"{r.RelatedPerson?.FirstName?.Value} {r.RelatedPerson?.LastName?.Value}",
                RelationType = r.RelationType?.Name ?? string.Empty
            }).ToList() ?? new List<RelationDto>()
        };
    }
}
