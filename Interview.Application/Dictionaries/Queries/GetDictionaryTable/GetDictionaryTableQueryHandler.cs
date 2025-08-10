using Interview.Application.Dictionaries.Queries.GetDictionaryTable;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Dictionaries.Queries.GetDictionaryTable;

/// <summary>
/// Dictionary სქემის ცხრილის ტიპების მიღების Handler
/// გამოიყენება ყველა dictionary სქემის ცხრილის ინფორმაციის მისაღებად
/// </summary>
public class GetDictionaryTableQueryHandler : IRequestHandler<GetDictionaryTableQuery, IEnumerable<DictionaryTableDto>>
{
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetDictionaryTableQueryHandler(IDictionaryRepository dictionaryRepository)
    {
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<IEnumerable<DictionaryTableDto>> Handle(GetDictionaryTableQuery request, CancellationToken cancellationToken)
    {
        var dictionaryTypes = await _dictionaryRepository.GetAvailableDictionaryTablesAsync();
        
        return dictionaryTypes.Select(type => new DictionaryTableDto
        {
            Name = type,
            DisplayName = type,
            Description = $"Dictionary ცხრილი: {type}"
        });
    }
}
