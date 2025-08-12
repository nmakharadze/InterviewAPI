using MediatR;
using Interview.Application.Repositories;

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
        var report = await _personRepository.GetPersonRelationsReportAsync(
            request.PersonId, 
            request.RelationTypeId, 
            cancellationToken);

        return report;
    }
}
