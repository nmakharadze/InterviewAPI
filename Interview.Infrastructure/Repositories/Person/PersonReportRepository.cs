using Microsoft.EntityFrameworkCore;
using Interview.Application.Repositories.Person;
using Interview.Infrastructure.Data;
using Interview.Application.Reports.Person.Queries.GetPersonRelationsReport;

namespace Interview.Infrastructure.Repositories.Person;

/// <summary>
/// ფიზიკური პირების რეპორტების Repository იმპლემენტაცია
/// Entity Framework-ის გამოყენებით
/// </summary>
public class PersonReportRepository : IPersonReportRepository
{
    private readonly InterviewDbContext _context;

    public PersonReportRepository(InterviewDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// ფიზიკური პირების ურთიერთობების რეპორტის მიღება
    /// </summary>
    public async Task<PersonReportDto> GetPersonRelationsReportAsync(int? personId = null, int? relationTypeId = null, CancellationToken cancellationToken = default)
    {
        //ფიზიკური პირების ძებნა ფილტრებით
        var personsQuery = _context.Persons.AsQueryable();
        if (personId.HasValue)
        {
            personsQuery = personsQuery.Where(p => p.Id == personId.Value);
        }

        var persons = await personsQuery
            .Include(p => p.Relations)
                .ThenInclude(r => r.RelationType)
            .ToListAsync(cancellationToken);

        //ფიზიკური პირების სტატისტიკის გენერირება
        var personStats = persons.Select(p => new PersonDataDto
        {
            PersonId = p.Id,
            PersonName = p.FirstName.Value,
            PersonLastName = p.LastName.Value,
            TotalRelations = p.Relations.Count,
            RelationsByType = p.Relations
                .Where(r => !relationTypeId.HasValue || r.RelationTypeId == relationTypeId.Value)
                .GroupBy(r => r.RelationType)
                .Select(g => new RelationReportDto
                {
                    RelationTypeId = g.Key.Id,
                    RelationTypeName = g.Key.Name,
                    Count = g.Count()
                })
                .ToList()
        }).ToList();

        // ზოგადი სტატისტიკა - ფილტრების გათვალისწინებით
        var totalPersons = persons.Count;
        var totalRelations = persons.Sum(p => p.Relations.Count);

        return new PersonReportDto
        {
            TotalPersons = totalPersons,
            TotalRelations = totalRelations,
            PersonStats = personStats
        };
    }
}

