using Interview.Application.Reports.Person.Queries.GetPersonRelationsReport;

namespace Interview.Application.Repositories.Person;

/// <summary>
/// ფიზიკური პირების რეპორტების Repository ინტერფეისი
/// მოიცავს რეპორტების ოპერაციებს
/// </summary>
public interface IPersonReportRepository
{
    /// <summary>
    /// ფიზიკური პირების ურთიერთობების რეპორტის მიღება
    /// </summary>
    Task<PersonReportDto> GetPersonRelationsReportAsync(int? personId = null, int? relationTypeId = null, CancellationToken cancellationToken = default);
}

