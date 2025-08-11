using MediatR;
using Interview.Application.Repositories;
using Interview.Application.Services;
using Microsoft.AspNetCore.Http;

namespace Interview.Application.Persons.Image.Commands.UploadImage
{
    public class UploadOrUpdateImageCommandHandler : IRequestHandler<UploadOrUpdateImageCommand, string>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public UploadOrUpdateImageCommandHandler(
            IFileStorageService fileStorageService,
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork)
        {
            _fileStorageService = fileStorageService;
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<string> Handle(UploadOrUpdateImageCommand request, CancellationToken cancellationToken)
        {
            // ვალიდაცია
            ValidateImageFile(request.ImageFile);
            
            // პიროვნების არსებობის შემოწმება
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person == null)
                throw new ArgumentException($"პიროვნება ID {request.PersonId} ვერ მოიძებნა");
            
            
            // ფაილის შენახვა (ახალი ან განახლება)
            var imagePath = await _fileStorageService.SaveFileAsync(request.ImageFile, request.PersonId);
            
            // მონაცემთა ბაზის განახლება
            person.UpdateImagePath(imagePath);
            await _personRepository.UpdateAsync(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return imagePath;
        }
        
        private void ValidateImageFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("ფაილი ცარიელია");
            
            // ფაილის ზომა (მაქსიმუმ 5MB)
            if (file.Length > 5 * 1024 * 1024)
                throw new ArgumentException("ფაილის ზომა არ უნდა აღემატებოდეს 5MB");
            
            // ფაილის ტიპი
            var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
            if (!allowedTypes.Contains(file.ContentType.ToLower()))
                throw new ArgumentException("არასწორი ფაილის ფორმატი");
            
            // ფაილის გაფართოება
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowedExtensions.Contains(extension))
                throw new ArgumentException("არასწორი ფაილის გაფართოება");
        }
    }
}
