using Interview.Application.Dictionaries.Commands.Create;
using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Dictionaries.Commands.Create;

/// <summary>
/// Dictionary სქემის ცხრილში ახალი ჩანაწერის შექმნის Handler
/// დინამიურად მუშაობს ყველა dictionary სქემის ცხრილთან
/// </summary>
public class CreateDictionaryCommandHandler : IRequestHandler<CreateDictionaryCommand, CreateDictionaryResultDto>
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDictionaryCommandHandler(IDictionaryRepository dictionaryRepository, IUnitOfWork unitOfWork)
    {
        _dictionaryRepository = dictionaryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateDictionaryResultDto> Handle(CreateDictionaryCommand request, CancellationToken cancellationToken)
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
        
        // Save changes using Unit of Work
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateDictionaryResultDto
        {
            Id = createdDictionary.Id,
            Name = createdDictionary.Name,
            DictionaryTableName = request.DictionaryTableName
        };
    }
}
