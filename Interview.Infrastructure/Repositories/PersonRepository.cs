using Microsoft.EntityFrameworkCore;
using Interview.Application.Repositories;
using Interview.Domain.Entities.Person;
using Interview.Infrastructure.Data;
using Interview.Application.Reports.Person.Queries.GetPersonRelationsReport;

namespace Interview.Infrastructure.Repositories;

/// <summary>
/// ფიზიკური პირების Repository იმპლემენტაცია
/// Entity Framework-ის გამოყენებით
/// </summary>
public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    private readonly InterviewDbContext _context;

    public PersonRepository(InterviewDbContext context) : base(context)
    {
        _context = context;
    }

    // ფიზიკური პირის CRUD ოპერაციები
    public new async Task<Person?> GetByIdAsync(int id)
    {
        return await _context.Persons
            .Include(p => p.Gender)
            .Include(p => p.City)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Person?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Persons
            .Include(p => p.Gender)
            .Include(p => p.City)
            .Include(p => p.PhoneNumbers)
                .ThenInclude(pn => pn.PhoneType)
            .Include(p => p.Relations)
                .ThenInclude(r => r.RelationType)
            .Include(p => p.Relations)
                .ThenInclude(r => r.RelatedPerson)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Person>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Persons
            .Include(p => p.City)
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Person?> GetByPersonalNumberAsync(string personalNumber)
    {
        return await _context.Persons
            .Include(p => p.Gender)
            .Include(p => p.City)
            .FirstOrDefaultAsync(p => p.PersonalNumber.Value == personalNumber);
    }

    public new async Task<bool> ExistsAsync(int id)
    {
        return await _context.Persons.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> ExistsByPersonalNumberAsync(string personalNumber)
    {
        return await _context.Persons.AnyAsync(p => p.PersonalNumber.Value == personalNumber);
    }

    public async Task<Person> AddAsync(Person person)
    {
        _context.Persons.Add(person);
        return person;
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        _context.Persons.Update(person);
        return person;
    }

    public async Task DeleteAsync(int id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person != null)
        {
            _context.Persons.Remove(person);
        }
    }

    

    // ძებნის ოპერაციები
    public async Task<IEnumerable<Person>> SearchAsync(string? searchTerm, int? cityId, int? genderId, int page, int pageSize)
    {
        var query = _context.Persons
            .Include(p => p.City)
            .AsQueryable();

        // ძებნის ფილტრის დამატება
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            query = query.Where(p => 
                p.FirstName.Value.ToLower().Contains(searchTerm) ||
                p.LastName.Value.ToLower().Contains(searchTerm) ||
                p.PersonalNumber.Value.Contains(searchTerm));
        }

        // ქალაქის ფილტრის დამატება
        if (cityId.HasValue)
        {
            query = query.Where(p => p.CityId == cityId.Value);
        }

        // სქესის ფილტრის დამატება
        if (genderId.HasValue)
        {
            query = query.Where(p => p.GenderId == genderId.Value);
        }

        return await query
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // ტელეფონის ნომრის ოპერაციები
    public async Task<PersonPhoneNumber?> GetPhoneNumberByIdAsync(int id)
    {
        return await _context.PersonPhoneNumbers
            .Include(pn => pn.PhoneType)
            .FirstOrDefaultAsync(pn => pn.Id == id);
    }

    public async Task<IEnumerable<PersonPhoneNumber>> GetPhoneNumbersByPersonIdAsync(int personId)
    {
        return await _context.PersonPhoneNumbers
            .Include(pn => pn.PhoneType)
            .Where(pn => pn.PersonId == personId)
            .ToListAsync();
    }

    public async Task<PersonPhoneNumber> AddPhoneNumberAsync(PersonPhoneNumber phoneNumber)
    {
        _context.PersonPhoneNumbers.Add(phoneNumber);
        return phoneNumber;
    }

    public async Task<PersonPhoneNumber> UpdatePhoneNumberAsync(PersonPhoneNumber phoneNumber)
    {
        _context.PersonPhoneNumbers.Update(phoneNumber);
        return phoneNumber;
    }

    public async Task DeletePhoneNumberAsync(int id)
    {
        var phoneNumber = await _context.PersonPhoneNumbers.FindAsync(id);
        if (phoneNumber != null)
        {
            _context.PersonPhoneNumbers.Remove(phoneNumber);
        }
    }

    // კავშირის ოპერაციები
    public async Task<PersonRelation?> GetRelationByIdAsync(int id)
    {
        return await _context.PersonRelations
            .Include(r => r.RelationType)
            .Include(r => r.RelatedPerson)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<PersonRelation>> GetRelationsByPersonIdAsync(int personId)
    {
        return await _context.PersonRelations
            .Include(r => r.RelationType)
            .Include(r => r.RelatedPerson)
            .Where(r => r.PersonId == personId)
            .ToListAsync();
    }

    public async Task<PersonRelation> AddRelationAsync(PersonRelation relation)
    {
        _context.PersonRelations.Add(relation);
        return relation;
    }

    public async Task<PersonRelation> UpdateRelationAsync(PersonRelation relation)
    {
        _context.PersonRelations.Update(relation);
        return relation;
    }

    public async Task DeleteRelationAsync(int id)
    {
        var relation = await _context.PersonRelations.FindAsync(id);
        if (relation != null)
        {
            _context.PersonRelations.Remove(relation);
        }
    }

    //ფიზიკური პირების რეპორტის ოპერაციები
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
