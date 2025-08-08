using Interview.Application.DTOs.Dictionary;
using Interview.Application.Queries.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Queries.Dictionary.Handlers;

/// <summary>
/// Dictionary სქემის ცხრილის ტიპების მიღების Handler
/// გამოიყენება ყველა dictionary სქემის ცხრილის ინფორმაციის მისაღებად
/// </summary>
public class GetDictionaryTypesQueryHandler : IRequestHandler<GetDictionaryTableQuery, IEnumerable<DictionaryTableDto>>
{
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetDictionaryTypesQueryHandler(IDictionaryRepository dictionaryRepository)
    {
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<IEnumerable<DictionaryTableDto>> Handle(GetDictionaryTableQuery request, CancellationToken cancellationToken)
    {
        var dictionaryTypes = await _dictionaryRepository.GetAvailableDictionaryTablesAsync();
        
        return dictionaryTypes.Select(type => new DictionaryTableDto
        {
            Name = type,
            DisplayName = type
        });
    }
}
