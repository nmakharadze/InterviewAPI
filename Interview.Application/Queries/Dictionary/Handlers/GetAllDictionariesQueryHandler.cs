using Interview.Application.DTOs.Dictionary;
using Interview.Application.Queries.Dictionary;
using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Queries.Dictionary.Handlers;

/// <summary>
/// ყველა Dictionary ჩანაწერის მიღების Handler
/// დინამიურად მუშაობს ყველა dictionary ტიპთან
/// </summary>
public class GetAllDictionariesQueryHandler : IRequestHandler<GetAllDictionariesQuery, IEnumerable<DictionaryDto>>
{
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetAllDictionariesQueryHandler(IDictionaryRepository dictionaryRepository)
    {
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<IEnumerable<DictionaryDto>> Handle(GetAllDictionariesQuery request, CancellationToken cancellationToken)
    {
        // Validate dictionary type
        await _dictionaryRepository.ValidateDictionaryTableAsync(request.DictionaryTableName);

        var dictionaries = await _dictionaryRepository.GetAllDictionariesAsync(request.DictionaryTableName);
        
        return dictionaries.Select(d => new DictionaryDto
        {
            Id = d.Id,
            Name = d.Name,
            DictionaryTableName = request.DictionaryTableName
        });
    }
}
