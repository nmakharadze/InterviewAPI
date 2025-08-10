using Interview.Application.Dictionaries.Queries.GetAll;
using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Dictionaries.Queries.GetAll;

/// <summary>
/// ყველა Dictionary ჩანაწერის მიღების Handler
/// დინამიურად მუშაობს ყველა dictionary ტიპთან
/// </summary>
public class GetAllDictionariesQueryHandler : IRequestHandler<GetAllDictionariesQuery, IEnumerable<DictionaryListDto>>
{
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetAllDictionariesQueryHandler(IDictionaryRepository dictionaryRepository)
    {
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<IEnumerable<DictionaryListDto>> Handle(GetAllDictionariesQuery request, CancellationToken cancellationToken)
    {
        // Validate dictionary type
        await _dictionaryRepository.ValidateDictionaryTableAsync(request.DictionaryTableName);

        var dictionaries = await _dictionaryRepository.GetAllDictionariesAsync(request.DictionaryTableName);
        
        return dictionaries.Select(d => new DictionaryListDto
        {
            Id = d.Id,
            Name = d.Name,
            DictionaryTableName = request.DictionaryTableName
        });
    }
}
