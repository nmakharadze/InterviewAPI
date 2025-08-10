namespace Interview.Application.Dictionaries.Commands.Update;

/// <summary>
/// dictionary სქემის ცხრილში ჩანაწერის განახლების დასაბრუნებელი DTO
/// </summary>
public class UpdateDictionaryResultDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    /// Dictionary - ცხრილის დასახელება
    public string DictionaryTableName { get; set; } = string.Empty;
}
