using MediatR;
using Interview.Application.Repositories;
using Interview.Application.Repositories.Person;

namespace Interview.Application.Reports.Person.Queries.GetPersonRelationsReport;

/// <summary>
/// ფიზიკური პირების კავშირების რეპორტის მოთხოვნის დამუშავება
/// </summary>
public class GetPersonRelationsReportQueryHandler : IRequestHandler<GetPersonRelationsReportQuery, PersonReportDto>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonRelationsReportQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<PersonReportDto> Handle(GetPersonRelationsReportQuery request, CancellationToken cancellationToken)
    {
        var report = await _personRepository.Reports.GetPersonRelationsReportAsync(
            request.PersonId, 
            request.RelationTypeId, 
            cancellationToken);

        return report;
    }
}
