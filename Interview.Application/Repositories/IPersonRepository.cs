using Interview.Domain.Entities.Person;
using Interview.Application.Reports.Person.Queries.GetPersonRelationsReport;
using Interview.Application.Persons.Queries.AdvancedSearch;

namespace Interview.Application.Repositories;

/// <summary>
/// ფიზიკური პირების Repository ინტერფეისი
/// მოიცავს ფიზიკურ პირთან დაკავშირებულ ყველა CRUD და სხვა საჭირო ოპერაციებს
/// </summary>
public interface IPersonRepository : IGenericRepository<Person>
{
    // Person CRUD operations (generic CRUD ხელმისაწვდომია IGenericRepository<Person>-იდან)
    Task<Person?> GetByIdWithDetailsAsync(int id);
    Task<IEnumerable<Person>> GetAllAsync(int page, int pageSize);
    Task<Person?> GetByPersonalNumberAsync(string personalNumber);
    Task<bool> ExistsByPersonalNumberAsync(string personalNumber);
    Task<Person> AddAsync(Person person);
    
    // Search operations
    Task<IEnumerable<Person>> SearchAsync(string? searchTerm, int? cityId, int? genderId, int page, int pageSize);
    Task<IEnumerable<Person>> AdvancedSearchAsync(AdvancedSearchFiltersDto filters);
    
    // Phone Number operations
    Task<Interview.Domain.Entities.Person.PersonPhoneNumber?> GetPhoneNumberByIdAsync(int id);
    Task<IEnumerable<Interview.Domain.Entities.Person.PersonPhoneNumber>> GetPhoneNumbersByPersonIdAsync(int personId);
    Task<Interview.Domain.Entities.Person.PersonPhoneNumber> AddPhoneNumberAsync(Interview.Domain.Entities.Person.PersonPhoneNumber phoneNumber);
    Task<Interview.Domain.Entities.Person.PersonPhoneNumber> UpdatePhoneNumberAsync(Interview.Domain.Entities.Person.PersonPhoneNumber phoneNumber);
    Task DeletePhoneNumberAsync(int id);
    
    // Relation operations
    Task<Interview.Domain.Entities.Person.PersonRelation?> GetRelationByIdAsync(int id);
    Task<IEnumerable<Interview.Domain.Entities.Person.PersonRelation>> GetRelationsByPersonIdAsync(int personId);
    Task<Interview.Domain.Entities.Person.PersonRelation> AddRelationAsync(Interview.Domain.Entities.Person.PersonRelation relation);
    Task<Interview.Domain.Entities.Person.PersonRelation> UpdateRelationAsync(Interview.Domain.Entities.Person.PersonRelation relation);
    Task DeleteRelationAsync(int id);
    
    // Report operations
    Task<PersonReportDto> GetPersonRelationsReportAsync(int? personId = null, int? relationTypeId = null, CancellationToken cancellationToken = default);
}
