using Interview.Domain.Entities.Base;

namespace Interview.Application.Repositories;

/// <summary>
/// Dictionary სქემის ცხრილების დინამიური ოპერაციების ინტერფეისი
/// </summary>
public interface IDictionaryRepository
{
    /// <summary>
    /// ამოწმებს ცხრილის ვალიდურობას
    /// </summary>
    Task ValidateDictionaryTableAsync(string tableName);

    /// <summary>
    /// აბრუნებს ყველა dictionary სქემის ცხრილის სახელს რაც დაკონფიგურირებულია ბაზაში
    /// </summary>
    Task<IEnumerable<string>> GetAvailableDictionaryTablesAsync();
    
    /// <summary>
    /// აბრუნებს ასემბლიდან EntityTypeName-ს tableName-ის მიხედვით
    /// </summary>
    /// <param name="tableName">ცხრილის სახელი</param>
    /// <returns>Entity კლასის სახელი</returns>
    Task<string?> GetDictionaryEntityTypeNameAsync(string tableName);

    /// <summary>
    /// ბაზიდან Entity type-ის მიღება DictionaryTableName-ის მიხედვით
    /// </summary>
    /// <param name="dictionaryType">Dictionary - ცხრილის დასახელება</param>
    /// <returns>Entity Type</returns>
    Task<Type> GetEntityTypeAsync(string dictionaryType);

    /// <summary>
    /// dictionary სქემის ცხრილიდან ყველა ჩანაწერის მიღება
    /// </summary>
    /// <param name="tableName">ცხრილის სახელი</param>
    /// <returns>Dictionary სქემის ცხრილიდან ჩანაწერების სია</returns>
    Task<IEnumerable<DictionaryBase>> GetAllDictionariesAsync(string tableName);

    /// <summary>
    /// dictionary სქემის ცხრილიდან კონკრეტული ჩანაწერის მიღება
    /// </summary>
    /// <param name="tableName">ცხრილის სახელი</param>
    /// <param name="id">ჩანაწერის ID</param>
    /// <returns>Dictionary ჩანაწერი</returns>
    Task<DictionaryBase?> GetDictionaryByIdAsync(string tableName, int id);
    
    /// <summary>
    /// Dictionary სქემის ცხრილში ჩანაწერის შექმნა
    /// </summary>
    /// <param name="tableName">ცხრილის სახელი</param>
    /// <param name="name">ჩანაწერის სახელი</param>
    /// <returns>შექმნილი dictionary ჩანაწერი</returns>
    Task<DictionaryBase> CreateDictionaryAsync(string tableName, string name);
    
    /// <summary>
    /// Dictionary სქემის ცხრილში არსებული ჩანაწერის განახლება
    /// </summary>
    /// <param name="tableName">ცხრილის სახელი</param>
    /// <param name="id">ჩანაწერის ID</param>
    /// <param name="name">ახალი სახელი</param>
    /// <returns>განახლებული dictionary ჩანაწერი</returns>
    Task<DictionaryBase> UpdateDictionaryAsync(string tableName, int id, string name);
    
    /// <summary>
    /// Dictionary სქემის ცხრილიდან ჩანაწერის წაშლა
    /// </summary>
    /// <param name="tableName">ცხრილის სახელი</param>
    /// <param name="id">ჩანაწერის ID</param>
    /// <returns>წაშლის შედეგი</returns>
    Task<bool> DeleteDictionaryAsync(string tableName, int id);
}
