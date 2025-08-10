using Interview.Application.Dictionaries.Queries.GetAll;
using MediatR;

namespace Interview.Application.Dictionaries.Queries.GetAll;

/// <summary>
/// Dictionary სქემის ცხრილიდან ყველა  ჩანაწერის მიღების Query
/// გამოიყენება CQRS პატერნში ჩანაწერების სიის მიღებისთვის
/// </summary>
public class GetAllDictionariesQuery : IRequest<IEnumerable<DictionaryListDto>>
{
    /// Dictionary - ცხრილის დასახელება
    public string DictionaryTableName { get; set; } = string.Empty;
}
