using Interview.Application.Dictionaries.Queries.GetDetail;
using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Dictionaries.Queries.GetDetail;

/// <summary>
/// Dictionary სქემის ცხრილიდან კონკრეტული  ჩანაწერის მიღების Handler
/// დინამიურად მუშაობს ყველა dictionary სქემის ცხრილთან
/// </summary>
public class GetDictionaryQueryHandler : IRequestHandler<GetDictionaryQuery, DictionaryDetailDto>
{
    private readonly IDictionaryRepository _dictionaryRepository;

    public GetDictionaryQueryHandler(IDictionaryRepository dictionaryRepository)
    {
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<DictionaryDetailDto> Handle(GetDictionaryQuery request, CancellationToken cancellationToken)
    {
        // Validate dictionary type
        await _dictionaryRepository.ValidateDictionaryTableAsync(request.DictionaryTableName);

        var dictionary = await _dictionaryRepository.GetDictionaryByIdAsync(request.DictionaryTableName, request.Id);
        
        if (dictionary == null)
        {
            throw new ArgumentException($" {request.DictionaryTableName} - ცხრილში ჩანაწერი მითითებული ID - {request.Id}-ით ჩანაწერი ვერ მოიძებნა: ");
        }

        return new DictionaryDetailDto
        {
            Id = dictionary.Id,
            Name = dictionary.Name,
            DictionaryTableName = request.DictionaryTableName,
            CreatedAt = dictionary.CreatedAt,
            UpdatedAt = dictionary.UpdatedAt
        };
    }
}
