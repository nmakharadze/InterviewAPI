using Interview.Application.DTOs.Dictionary;
using Interview.Application.Queries.Dictionary;
using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Queries.Dictionary.Handlers;

/// <summary>
/// Dictionary სქემის ცხრილიდან კონკრეტული  ჩანაწერის მიღების Handler
/// დინამიურად მუშაობს ყველა dictionary სქემის ცხრილთან
/// </summary>
public class GetDictionaryQueryHandler : IRequestHandler<GetDictionaryQuery, DictionaryDto>
{
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetDictionaryQueryHandler(IDictionaryRepository dictionaryRepository)
    {
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<DictionaryDto> Handle(GetDictionaryQuery request, CancellationToken cancellationToken)
    {
        // Validate dictionary type
        await _dictionaryRepository.ValidateDictionaryTableAsync(request.DictionaryTableName);

        var dictionary = await _dictionaryRepository.GetDictionaryByIdAsync(request.DictionaryTableName, request.Id);
        
        if (dictionary == null)
        {
            throw new ArgumentException($" {request.DictionaryTableName} - ცხრილში ჩანაწერი მითითებული ID - {request.Id}-ით ჩანაწერი ვერ მოიძებნა: ");
        }

        return new DictionaryDto
        {
            Id = dictionary.Id,
            Name = dictionary.Name,
            DictionaryTableName = request.DictionaryTableName
        };
    }
}
