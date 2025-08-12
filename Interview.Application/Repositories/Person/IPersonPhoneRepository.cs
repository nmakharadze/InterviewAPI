using Interview.Domain.Entities.Person;

namespace Interview.Application.Repositories.Person;

/// <summary>
/// ფიზიკური პირების ტელეფონის ნომრების Repository ინტერფეისი
/// მოიცავს ტელეფონის ნომრების ოპერაციებს
/// </summary>
public interface IPersonPhoneRepository
{
    /// <summary>
    /// ტელეფონის ნომრის მიღება ID-ით
    /// </summary>
    Task<PersonPhoneNumber?> GetByIdAsync(int id);
    
    /// <summary>
    /// ფიზიკური პირის ყველა ტელეფონის ნომრის მიღება
    /// </summary>
    Task<IEnumerable<PersonPhoneNumber>> GetByPersonIdAsync(int personId);
    
    /// <summary>
    /// ახალი ტელეფონის ნომრის დამატება
    /// </summary>
    Task<PersonPhoneNumber> AddAsync(PersonPhoneNumber phoneNumber);
    
    /// <summary>
    /// ტელეფონის ნომრის განახლება
    /// </summary>
    Task<PersonPhoneNumber> UpdateAsync(PersonPhoneNumber phoneNumber);
    
    /// <summary>
    /// ტელეფონის ნომრის წაშლა
    /// </summary>
    Task DeleteAsync(int id);
}

