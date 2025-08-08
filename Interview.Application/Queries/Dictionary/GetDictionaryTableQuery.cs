using Interview.Application.DTOs.Dictionary;
using MediatR;

namespace Interview.Application.Queries.Dictionary;

/// <summary>
/// Dictionary სქემის ცხრილის ტიპების მიღების Query
/// გამოიყენება ყველა dictionary სქემის ცხრილის დასახელების მისაღებად
/// </summary>
public class GetDictionaryTableQuery : IRequest<IEnumerable<DictionaryTableDto>>
{
}
