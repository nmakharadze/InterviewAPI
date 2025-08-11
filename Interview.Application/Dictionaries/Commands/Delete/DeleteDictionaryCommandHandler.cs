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
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDictionaryCommandHandler(IDictionaryRepository dictionaryRepository, IUnitOfWork unitOfWork)
    {
        _dictionaryRepository = dictionaryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteDictionaryCommand request, CancellationToken cancellationToken)
    {
        // Validate dictionary table name
        await _dictionaryRepository.ValidateDictionaryTableAsync(request.DictionaryTableName);

        // Delete record from dictionary table
        var result = await _dictionaryRepository.DeleteDictionaryAsync(request.DictionaryTableName, request.Id);
        
        // Save changes using Unit of Work
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return result;
    }
}
