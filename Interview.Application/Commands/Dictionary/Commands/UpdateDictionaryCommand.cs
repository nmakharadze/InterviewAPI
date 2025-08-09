using Interview.Application.DTOs.Dictionary;
using MediatR;

namespace Interview.Application.Commands.Dictionary;

/// <summary>
/// Dictionary სქემის ცხრილში არსებული ჩანაწერის განახლების Command
/// გამოიყენება CQRS პატერნში არსებული ჩანაწერის განახლებისთვის
/// </summary>
public class UpdateDictionaryCommand : IRequest<DictionaryDto>
{
    /// <summary>
    /// Dictionary - ცხრილის დასახელება
    /// </summary>
    public string DictionaryTableName { get; set; } = string.Empty;
    
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
}
