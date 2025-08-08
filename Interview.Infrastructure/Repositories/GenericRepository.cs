using System.Linq.Expressions;
using Interview.Application.Repositories;
using Interview.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Interview.Infrastructure.Repositories;

/// <summary>
/// ზოგადი repository implementation ნებისმიერი entity-სთვის
/// </summary>
/// <typeparam name="T">Entity ტიპი</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly InterviewDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(InterviewDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <summary>
    /// ყველა ჩანაწერის მიღება
    /// </summary>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    /// <summary>
    /// კონკრეტული ჩანაწერის მიღება ID-ის მიხედვით
    /// </summary>
    /// <param name="id">Entity-ის ID</param>
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// ახალი ჩანაწერის შექმნა
    /// </summary>
    /// <param name="entity">შესაქმნელი entity</param>
    public async Task<T> CreateAsync(T entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// არსებული ჩანაწერის განახლება
    /// </summary>
    /// <param name="entity">განასახლებელი entity</param>
    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// ჩანაწერის წაშლა ID-ის მიხედვით
    /// </summary>
    /// <param name="id">წასაშლელი entity-ის ID</param>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// ჩანაწერის არსებობის შემოწმება ID-ის მიხედვით
    /// </summary>
    /// <param name="id">Entity-ის ID</param>
    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.FindAsync(id) != null;
    }

    /// <summary>
    /// ჩანაწერების მიღება პირობის მიხედვით
    /// </summary>
    /// <param name="predicate">ფილტრის პირობა</param>
    public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
}
