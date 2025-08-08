using System.Linq.Expressions;

namespace Interview.Application.Repositories;

/// <summary>
/// ზოგადი repository interface ნებისმიერი entity-სთვის
/// გამოყენებადი ყველა entity-სთვის (Person, City, Gender, etc.)
/// </summary>
/// <typeparam name="T">Entity ტიპი</typeparam>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// ყველა ჩანაწერის მიღება
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    /// კონკრეტული ჩანაწერის მიღება ID-ის მიხედვით
    /// </summary>
    /// <param name="id">Entity-ის ID</param>
    Task<T?> GetByIdAsync(int id);
    
    /// <summary>
    /// ახალი ჩანაწერის შექმნა
    /// </summary>
    /// <param name="entity">შესაქმნელი entity</param>
    Task<T> CreateAsync(T entity);
    
    /// <summary>
    /// არსებული ჩანაწერის განახლება
    /// </summary>
    /// <param name="entity">განასაახლებელი entity</param>
    Task<T> UpdateAsync(T entity);
    
    /// <summary>
    /// ჩანაწერის წაშლა ID-ის მიხედვით
    /// </summary>
    /// <param name="id">წასაშლელი entity-ის ID</param>
    Task<bool> DeleteAsync(int id);
    
    /// <summary>
    /// ჩანაწერის არსებობის შემოწმება ID-ის მიხედვით
    /// </summary>
    /// <param name="id">Entity-ის ID</param>
    Task<bool> ExistsAsync(int id);
    
    /// <summary>
    /// ჩანაწერების მიღება პირობის მიხედვით
    /// </summary>
    /// <param name="predicate">ფილტრის პირობა</param>
    Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);
}
