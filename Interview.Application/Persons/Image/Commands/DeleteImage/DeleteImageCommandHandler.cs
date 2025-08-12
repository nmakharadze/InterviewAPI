using MediatR;
using Interview.Application.Repositories;
using Interview.Application.Repositories.Person;
using Interview.Application.Services;

namespace Interview.Application.Persons.Image.Commands.DeleteImage
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, bool>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteImageCommandHandler(
            IFileStorageService fileStorageService,
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork)
        {
            _fileStorageService = fileStorageService;
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<bool> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            // ფიზიკური პირის არსებობის შემოწმება
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person == null)
                throw new ArgumentException($"ფიზიკური პირის ID {request.PersonId} ვერ მოიძებნა");
            
            // ფაილის არსებობის შემოწმება
            var fileExists = _fileStorageService.FileExists(request.PersonId);
            if (!fileExists)
                throw new ArgumentException($"ფიზიკური პირის ID {request.PersonId}-ით ფაილი არ არსებობს");
            
            // ფაილის წაშლა
            _fileStorageService.DeleteFile(request.PersonId);
            
            // მონაცემთა ბაზის განახლება
            person.UpdateImagePath(null);
            await _personRepository.UpdateAsync(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}
