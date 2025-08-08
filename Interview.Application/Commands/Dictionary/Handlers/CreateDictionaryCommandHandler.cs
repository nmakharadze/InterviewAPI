using Interview.Application.Commands.Dictionary;
using Interview.Application.DTOs.Dictionary;
using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Commands.Dictionary.Handlers;

/// <summary>
/// Dictionary სქემის ცხრილში ახალი ჩანაწერის შექმნის Handler
/// დინამიურად მუშაობს ყველა dictionary სქემის ცხრილთან
/// </summary>
public class CreateDictionaryCommandHandler : IRequestHandler<CreateDictionaryCommand, DictionaryDto>
{
    private readonly IDictionaryRepository _dictionaryRepository;

    public CreateDictionaryCommandHandler(IDictionaryRepository dictionaryRepository)
    {
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<DictionaryDto> Handle(CreateDictionaryCommand request, CancellationToken cancellationToken)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("სახელის მითითება სავალდებულოა");
        }

        // Validate dictionary type
        await _dictionaryRepository.ValidateDictionaryTableAsync(request.DictionaryTableName);

        // Create dictionary
        var createdDictionary = await _dictionaryRepository.CreateDictionaryAsync(request.DictionaryTableName, request.Name);

        return new DictionaryDto
        {
            Id = createdDictionary.Id,
            Name = createdDictionary.Name,
            DictionaryTableName = request.DictionaryTableName
        };
    }
}
