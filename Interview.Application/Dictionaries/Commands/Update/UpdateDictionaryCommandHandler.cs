using Interview.Application.Dictionaries.Commands.Update;
using Interview.Application.Dictionaries.Queries.GetAll;
using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Dictionaries.Commands.Update;

/// <summary>
/// Dictionary სქემის ცხრილში არსებული ჩანაწერის განახლების Handler
/// დინამიურად მუშაობს ყველა dictionary სქემის ცხრილთან
/// </summary>
public class UpdateDictionaryCommandHandler : IRequestHandler<UpdateDictionaryCommand, UpdateDictionaryResultDto>
{
    private readonly IDictionaryRepository _dictionaryRepository;

    public UpdateDictionaryCommandHandler(IDictionaryRepository dictionaryRepository)
    {
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<UpdateDictionaryResultDto> Handle(UpdateDictionaryCommand request, CancellationToken cancellationToken)
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

        return new UpdateDictionaryResultDto
        {
            Id = updatedDictionary.Id,
            Name = updatedDictionary.Name,
            DictionaryTableName = request.DictionaryTableName
        };
    }
}
