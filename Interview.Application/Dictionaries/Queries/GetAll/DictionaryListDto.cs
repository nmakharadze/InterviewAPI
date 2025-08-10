namespace Interview.Application.Dictionaries.Queries.GetAll;

/// <summary>
/// dictionary სქემის ცხრილიდან ყველა ჩანაწერის დაბრუნება
/// გამოიყენება API-ში მონაცემების გადასაცემად
/// </summary>
public class DictionaryListDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    /// Dictionary - ცხრილის დასახელება
    public string DictionaryTableName { get; set; } = string.Empty;
}
