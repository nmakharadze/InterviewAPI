namespace Interview.Application.Reports.Person.Queries.GetPersonRelationsReport;

/// <summary>
/// ფიზიკური პირების რეპორტის DTO
/// </summary>
public class PersonReportDto
{
    public int TotalPersons { get; set; }
    public int TotalRelations { get; set; }
    public List<PersonDataDto> PersonStats { get; set; } = new();
}

/// <summary>
/// ფიზიკური პირის მონაცემების DTO
/// </summary>
public class PersonDataDto
{
    public int PersonId { get; set; }
    public string PersonName { get; set; } = string.Empty;
    public string PersonLastName { get; set; } = string.Empty;
    public int TotalRelations { get; set; }
    public List<RelationReportDto> RelationsByType { get; set; } = new();
}

/// <summary>
/// ფიზიკური პირის კავშირის რეპორტის DTO
/// </summary>
public class RelationReportDto
{
    public int RelationTypeId { get; set; }
    public string RelationTypeName { get; set; } = string.Empty;
    public int Count { get; set; }
}
