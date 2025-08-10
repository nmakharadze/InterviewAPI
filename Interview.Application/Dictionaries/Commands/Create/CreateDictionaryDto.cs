namespace Interview.Application.Dictionaries.Commands.Create;

/// <summary>
/// Dictionary სქემის ცხრილში ახალი ჩანაწერის შექმნის DTO
/// გამოიყენება არჩეულ ცხრილში ახალი ჩანაწერის შექმნისთვის
/// </summary>
public class CreateDictionaryDto
{
    public string Name { get; set; } = string.Empty;
}
