using Interview.Application.DTOs.Dictionary;
using MediatR;

namespace Interview.Application.Queries.Dictionary;

/// <summary>
/// Dictionary სქემის კონკრეტული  ცხრილიდან ჩანაწერის მიღების Query
/// გამოიყენება CQRS პატერნში ჩანაწერის მიღებისთვის
/// </summary>
public class GetDictionaryQuery : IRequest<DictionaryDto>
{
    /// Dictionary - ცხრილის დასახელება
    public string DictionaryTableName { get; set; } = string.Empty;
    
    public int Id { get; set; }
}
