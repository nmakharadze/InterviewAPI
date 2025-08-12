using Interview.Domain.Entities.Person;

namespace Interview.Application.Repositories.Person;

/// <summary>
/// ფიზიკური პირების ურთიერთობების Repository ინტერფეისი
/// მოიცავს ურთიერთობების ოპერაციებს
/// </summary>
public interface IPersonRelationRepository
{
    /// <summary>
    /// ურთიერთობის მიღება ID-ით
    /// </summary>
    Task<PersonRelation?> GetByIdAsync(int id);
    
    /// <summary>
    /// ფიზიკური პირის ყველა ურთიერთობის მიღება
    /// </summary>
    Task<IEnumerable<PersonRelation>> GetByPersonIdAsync(int personId);
    
    /// <summary>
    /// ახალი ურთიერთობის დამატება
    /// </summary>
    Task<PersonRelation> AddAsync(PersonRelation relation);
    
    /// <summary>
    /// ურთიერთობის განახლება
    /// </summary>
    Task<PersonRelation> UpdateAsync(PersonRelation relation);
    
    /// <summary>
    /// ურთიერთობის წაშლა
    /// </summary>
    Task DeleteAsync(int id);
}
