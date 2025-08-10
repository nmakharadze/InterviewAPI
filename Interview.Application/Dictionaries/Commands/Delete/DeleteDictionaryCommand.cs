using MediatR;

namespace Interview.Application.Dictionaries.Commands.Delete;

/// <summary>
/// Dictionary სქემის ცხრილიდან ჩანაწერის წაშლის Command
/// გამოიყენება CQRS პატერნში ცხრილიდან ჩანაწერის წაშლისთვის
/// </summary>
public class DeleteDictionaryCommand : IRequest<bool>
{
    /// <summary>
    /// Dictionary - ცხრილის დასახელება
    /// </summary>
    public string DictionaryTableName { get; set; } = string.Empty;
    
    public int Id { get; set; }
}
