using MediatR;

namespace Interview.Application.Reports.Person.Queries.GetPersonRelationsReport;

/// <summary>
/// ფიზიკური პირების კავშირების რეპორტის მოთხოვნა
/// </summary>
public class GetPersonRelationsReportQuery : IRequest<PersonReportDto>
{
    /// <summary>
    /// კონკრეტული ფიზიკური პირის ID
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// კონკრეტული კავშირის ტიპის ID
    /// </summary>
    public int? RelationTypeId { get; set; }
}
