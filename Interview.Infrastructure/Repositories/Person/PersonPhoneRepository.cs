using Microsoft.EntityFrameworkCore;
using Interview.Application.Repositories.Person;
using Interview.Domain.Entities.Person;
using Interview.Infrastructure.Data;

namespace Interview.Infrastructure.Repositories.Person;

/// <summary>
/// ფიზიკური პირების ტელეფონის ნომრების Repository იმპლემენტაცია
/// Entity Framework-ის გამოყენებით
/// </summary>
public class PersonPhoneRepository : IPersonPhoneRepository
{
    private readonly InterviewDbContext _context;

    public PersonPhoneRepository(InterviewDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// ტელეფონის ნომრის მიღება ID-ით
    /// </summary>
    public async Task<PersonPhoneNumber?> GetByIdAsync(int id)
    {
        return await _context.PersonPhoneNumbers
            .Include(pn => pn.PhoneType)
            .FirstOrDefaultAsync(pn => pn.Id == id);
    }

    /// <summary>
    /// ფიზიკური პირის ყველა ტელეფონის ნომრის მიღება
    /// </summary>
    public async Task<IEnumerable<PersonPhoneNumber>> GetByPersonIdAsync(int personId)
    {
        return await _context.PersonPhoneNumbers
            .Include(pn => pn.PhoneType)
            .Where(pn => pn.PersonId == personId)
            .ToListAsync();
    }

    /// <summary>
    /// ახალი ტელეფონის ნომრის დამატება
    /// </summary>
    public async Task<PersonPhoneNumber> AddAsync(PersonPhoneNumber phoneNumber)
    {
        _context.PersonPhoneNumbers.Add(phoneNumber);
        return phoneNumber;
    }

    /// <summary>
    /// ტელეფონის ნომრის განახლება
    /// </summary>
    public async Task<PersonPhoneNumber> UpdateAsync(PersonPhoneNumber phoneNumber)
    {
        _context.PersonPhoneNumbers.Update(phoneNumber);
        return phoneNumber;
    }

    /// <summary>
    /// ტელეფონის ნომრის წაშლა
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var phoneNumber = await _context.PersonPhoneNumbers.FindAsync(id);
        if (phoneNumber != null)
        {
            _context.PersonPhoneNumbers.Remove(phoneNumber);
        }
    }
}

