using Microsoft.EntityFrameworkCore;
using Interview.Application.Repositories.Person;
using Interview.Domain.Entities.Person;
using Interview.Infrastructure.Data;

namespace Interview.Infrastructure.Repositories.Person;

/// <summary>
/// ფიზიკური პირების ურთიერთობების Repository იმპლემენტაცია
/// Entity Framework-ის გამოყენებით
/// </summary>
public class PersonRelationRepository : IPersonRelationRepository
{
    private readonly InterviewDbContext _context;

    public PersonRelationRepository(InterviewDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// ურთიერთობის მიღება ID-ით
    /// </summary>
    public async Task<PersonRelation?> GetByIdAsync(int id)
    {
        return await _context.PersonRelations
            .Include(r => r.RelationType)
            .Include(r => r.RelatedPerson)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <summary>
    /// ფიზიკური პირის ყველა ურთიერთობის მიღება
    /// </summary>
    public async Task<IEnumerable<PersonRelation>> GetByPersonIdAsync(int personId)
    {
        return await _context.PersonRelations
            .Include(r => r.RelationType)
            .Include(r => r.RelatedPerson)
            .Where(r => r.PersonId == personId)
            .ToListAsync();
    }

    /// <summary>
    /// ახალი ურთიერთობის დამატება
    /// </summary>
    public async Task<PersonRelation> AddAsync(PersonRelation relation)
    {
        _context.PersonRelations.Add(relation);
        return relation;
    }

    /// <summary>
    /// ურთიერთობის განახლება
    /// </summary>
    public async Task<PersonRelation> UpdateAsync(PersonRelation relation)
    {
        _context.PersonRelations.Update(relation);
        return relation;
    }

    /// <summary>
    /// ურთიერთობის წაშლა
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var relation = await _context.PersonRelations.FindAsync(id);
        if (relation != null)
        {
            _context.PersonRelations.Remove(relation);
        }
    }
}
