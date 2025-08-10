using System;

namespace Interview.Application.Dictionaries.Queries.GetDetail;

/// <summary>
/// DTO კონკრეტული dictionary ჩანაწერის დეტალური ინფორმაციისთვის
/// გამოიყენება GetDictionary Query-ში ერთი კონკრეტული ჩანაწერის მიღებისთვის
/// </summary>
public class DictionaryDetailDto
{
    /// <summary>
    /// ჩანაწერის ID
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// ჩანაწერის დასახელება
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Dictionary ცხრილის დასახელება
    /// </summary>
    public string DictionaryTableName { get; set; } = string.Empty;
    
    /// <summary>
    /// შექმნის თარიღი
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// განახლების თარიღი
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
