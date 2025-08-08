using Interview.Application.Commands.Dictionary;
using Interview.Application.DTOs.Dictionary;
using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Commands.Dictionary.Handlers;

/// <summary>
/// Dictionary სქემის ცხრილში არსებული ჩანაწერის განახლების Handler
/// დინამიურად მუშაობს ყველა dictionary სქემის ცხრილთან
/// </summary>
public class UpdateDictionaryCommandHandler : IRequestHandler<UpdateDictionaryCommand, DictionaryDto>
{
    private readonly IDictionaryRepository _dictionaryRepository;

    public UpdateDictionaryCommandHandler(IDictionaryRepository dictionaryRepository)
    {
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<DictionaryDto> Handle(UpdateDictionaryCommand request, CancellationToken cancellationToken)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("სახელის მითითება სავალდებულოა");
        }

        // Validate dictionary type
        await _dictionaryRepository.ValidateDictionaryTableAsync(request.DictionaryTableName);

        // Update dictionary
        var updatedDictionary = await _dictionaryRepository.UpdateDictionaryAsync(request.DictionaryTableName, request.Id, request.Name);

        return new DictionaryDto
        {
            Id = updatedDictionary.Id,
            Name = updatedDictionary.Name,
            DictionaryTableName = request.DictionaryTableName
        };
    }
}
