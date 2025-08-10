using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Dictionaries.Commands.Delete;

/// <summary>
/// Dictionary სქემის ცხრილიდან ჩანაწერის წაშლის Handler
/// დინამიურად მუშაობს ყველა dictionary სქემის ცხრილთან
/// </summary>
public class DeleteDictionaryCommandHandler : IRequestHandler<DeleteDictionaryCommand, bool>
{
    private readonly IDictionaryRepository _dictionaryRepository;

    public DeleteDictionaryCommandHandler(IDictionaryRepository dictionaryRepository)
    {
        _dictionaryRepository = dictionaryRepository;
    }

    public async Task<bool> Handle(DeleteDictionaryCommand request, CancellationToken cancellationToken)
    {
        // Validate dictionary table name
        await _dictionaryRepository.ValidateDictionaryTableAsync(request.DictionaryTableName);

        // Delete record from dictionary table
        return await _dictionaryRepository.DeleteDictionaryAsync(request.DictionaryTableName, request.Id);
    }
}
