using Interview.Application.DTOs.Dictionary;
using MediatR;

namespace Interview.Application.Queries.Dictionary;

/// <summary>
/// Dictionary სქემის ცხრილიდან ყველა  ჩანაწერის მიღების Query
/// გამოიყენება CQRS პატერნში ჩანაწერების სიის მიღებისთვის
/// </summary>
public class GetAllDictionariesQuery : IRequest<IEnumerable<DictionaryDto>>
{
    /// Dictionary - ცხრილის დასახელება
    public string DictionaryTableName { get; set; } = string.Empty;
}
