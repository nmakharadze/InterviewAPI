using Interview.Domain.Entities.Person;

namespace Interview.Application.Repositories;

/// <summary>
/// ფიზიკური პირების Repository ინტერფეისი
/// მოიცავს ყველა CRUD ოპერაციას ფიზიკური პირებისთვის
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
    
    // Phone Number operations
    Task<PersonPhoneNumber?> GetPhoneNumberByIdAsync(int id);
    Task<IEnumerable<PersonPhoneNumber>> GetPhoneNumbersByPersonIdAsync(int personId);
    Task<PersonPhoneNumber> AddPhoneNumberAsync(PersonPhoneNumber phoneNumber);
    Task<PersonPhoneNumber> UpdatePhoneNumberAsync(PersonPhoneNumber phoneNumber);
    Task DeletePhoneNumberAsync(int id);
    
    // Relation operations
    Task<PersonRelation?> GetRelationByIdAsync(int id);
    Task<IEnumerable<PersonRelation>> GetRelationsByPersonIdAsync(int personId);
    Task<PersonRelation> AddRelationAsync(PersonRelation relation);
    Task<PersonRelation> UpdateRelationAsync(PersonRelation relation);
    Task DeleteRelationAsync(int id);
}
