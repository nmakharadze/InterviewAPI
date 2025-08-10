namespace Interview.Application.Dictionaries.Queries.GetDictionaryTable;

/// <summary>
/// Dictionary სქემის ცხრილის ტიპის DTO
/// გამოიყენება API-ში dictionary სქემის ცხრილის ტიპების დასაბრუნებლად
/// </summary>
public class DictionaryTableDto
{
    public string Name { get; set; } = string.Empty;
    
    public string DisplayName { get; set; } = string.Empty;
    
    /// Dictionary ტიპის აღწერა - ცხრილის დასახელება
    public string Description { get; set; } = string.Empty;
}
