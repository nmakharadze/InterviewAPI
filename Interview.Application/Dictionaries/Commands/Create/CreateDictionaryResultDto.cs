namespace Interview.Application.Dictionaries.Commands.Create;

/// <summary>
/// dictionary სქემის ცხრილში დამატებული ახალი ჩანაწერის დასაბრუნებელი DTO
/// </summary>
public class CreateDictionaryResultDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    /// Dictionary - ცხრილის დასახელება
    public string DictionaryTableName { get; set; } = string.Empty;
}
