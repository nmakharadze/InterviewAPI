namespace Interview.Application.DTOs.Dictionary;

/// <summary>
/// ზოგადი DTO ყველა dictionary სქემის ცხრილისთვის
/// გამოიყენება API-ში მონაცემების გადასაცემად
/// </summary>
public class DictionaryDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    /// Dictionary - ცხრილის დასახელება
    public string DictionaryTableName { get; set; } = string.Empty;
}
