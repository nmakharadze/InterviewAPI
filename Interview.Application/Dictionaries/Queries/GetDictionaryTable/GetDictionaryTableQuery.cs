using Interview.Application.Dictionaries.Queries.GetDictionaryTable;
using MediatR;

namespace Interview.Application.Dictionaries.Queries.GetDictionaryTable;

/// <summary>
/// Dictionary სქემის ცხრილის ტიპების მიღების Query
/// გამოიყენება ყველა dictionary სქემის ცხრილის დასახელების მისაღებად
/// </summary>
public class GetDictionaryTableQuery : IRequest<IEnumerable<DictionaryTableDto>>
{
}
