using Interview.Application.Dictionaries.Queries.GetDetail;
using MediatR;

namespace Interview.Application.Dictionaries.Queries.GetDetail;

/// <summary>
/// Dictionary სქემის კონკრეტული  ცხრილიდან ჩანაწერის მიღების Query
/// გამოიყენება CQRS პატერნში ჩანაწერის მიღებისთვის
/// </summary>
public class GetDictionaryQuery : IRequest<DictionaryDetailDto>
{
    /// Dictionary - ცხრილის დასახელება
    public string DictionaryTableName { get; set; } = string.Empty;
    
    public int Id { get; set; }
}
