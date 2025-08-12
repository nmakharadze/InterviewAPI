using Microsoft.EntityFrameworkCore;
using Interview.Application.Repositories;
using Interview.Application.Repositories.Person;
using Interview.Domain.Entities.Person;
using Interview.Infrastructure.Data;
using PersonEntity = Interview.Domain.Entities.Person.Person;

namespace Interview.Infrastructure.Repositories.Person;

/// <summary>
/// ფიზიკური პირების Repository იმპლემენტაცია
/// Entity Framework-ის გამოყენებით
/// </summary>
public class PersonRepository : GenericRepository<PersonEntity>, IPersonRepository
{
    private readonly InterviewDbContext _context;

    public PersonRepository(InterviewDbContext context) : base(context)
    {
        _context = context;
        Search = new PersonSearchRepository(context);
        Phones = new PersonPhoneRepository(context);
        Relations = new PersonRelationRepository(context);
        Reports = new PersonReportRepository(context);
    }

    // სპეციალიზებული Repository-ები
    public IPersonSearchRepository Search { get; }
    public IPersonPhoneRepository Phones { get; }
    public IPersonRelationRepository Relations { get; }
    public IPersonReportRepository Reports { get; }

    // ფიზიკური პირის CRUD ოპერაციები
    public new async Task<PersonEntity?> GetByIdAsync(int id)
    {
        return await _context.Persons
            .Include(p => p.Gender)
            .Include(p => p.City)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PersonEntity?> GetByIdWithDetailsAsync(int id)
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

    public async Task<IEnumerable<PersonEntity>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Persons
            .Include(p => p.City)
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<PersonEntity?> GetByPersonalNumberAsync(string personalNumber)
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

    public async Task<PersonEntity> AddAsync(PersonEntity person)
    {
        _context.Persons.Add(person);
        return person;
    }

    public async Task<PersonEntity> UpdateAsync(PersonEntity person)
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










}
