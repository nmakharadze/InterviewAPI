using Interview.Application.DTOs.Dictionary;
using MediatR;

namespace Interview.Application.Commands.Dictionary;

/// <summary>
/// Dictionary სქემის ცხრილში ახალი ჩანაწერის შექმნის Command
/// გამოიყენება CQRS პატერნში ახალი ჩანაწერის შექმნისთვის
/// </summary>
public class CreateDictionaryCommand : IRequest<DictionaryDto>
{
    /// <summary>
    /// Dictionary - ცხრილის დასახელება
    /// </summary>
    public string DictionaryTableName { get; set; } = string.Empty;
    /// <summary>
    /// ცნობარის სახელი
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
