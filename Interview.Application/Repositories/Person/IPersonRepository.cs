using Interview.Domain.Entities.Person;
using Interview.Application.Repositories;
using PersonEntity = Interview.Domain.Entities.Person.Person;

namespace Interview.Application.Repositories.Person;

/// <summary>
/// ფიზიკური პირების Repository ინტერფეისი
/// მოიცავს ფიზიკურ პირთან დაკავშირებულ ყველა CRUD
/// </summary>
public interface IPersonRepository : IGenericRepository<PersonEntity>
{
    // Person CRUD operations (generic CRUD ხელმისაწვდომია IGenericRepository<PersonEntity>-იდან)
    Task<PersonEntity?> GetByIdWithDetailsAsync(int id);
    Task<IEnumerable<PersonEntity>> GetAllAsync(int page, int pageSize);
    Task<PersonEntity?> GetByPersonalNumberAsync(string personalNumber);
    Task<bool> ExistsByPersonalNumberAsync(string personalNumber);
    Task<PersonEntity> AddAsync(PersonEntity person);
    
    // სპეციალიზებული Repository-ები
    IPersonSearchRepository Search { get; }
    IPersonPhoneRepository Phones { get; }
    IPersonRelationRepository Relations { get; }
    IPersonReportRepository Reports { get; }
}
