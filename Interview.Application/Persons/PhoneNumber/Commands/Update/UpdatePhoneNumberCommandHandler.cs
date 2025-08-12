using System;
using System.Threading;
using System.Threading.Tasks;
using Interview.Domain.Entities.Person;
using Interview.Domain.Entities.Dictionary;
using Interview.Domain.ValueObjects;
using Interview.Application.Repositories;
using Interview.Application.Repositories.Person;
using MediatR;

namespace Interview.Application.Persons.PhoneNumber.Commands.Update
{
    /// <summary>
    /// Handler ტელეფონის ნომრის განახლებისთვის
    /// </summary>
    public class UpdatePhoneNumberCommandHandler : IRequestHandler<UpdatePhoneNumberCommand, bool>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PhoneType> _phoneTypeRepository;

        public UpdatePhoneNumberCommandHandler(
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork,
            IGenericRepository<PhoneType> phoneTypeRepository)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _phoneTypeRepository = phoneTypeRepository;
        }

        public async Task<bool> Handle(UpdatePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            // ტელეფონის ნომრის პოვნა
            var phoneNumber = await _personRepository.Phones.GetByIdAsync(request.Id);
            if (phoneNumber == null)
            {
                return false;
            }

            // ტელეფონის ნომრის ტიპის ვალიდაცია
            if (request.PhoneTypeId.HasValue)
            {
                var phoneTypeExists = await _phoneTypeRepository.ExistsAsync(request.PhoneTypeId.Value);
                if (!phoneTypeExists)
                {
                    throw new ArgumentException($"ტელეფონის ნომრის ტიპი ID {request.PhoneTypeId.Value} არ არსებობს");
                }
                phoneNumber.PhoneTypeId = request.PhoneTypeId.Value;
            }

            // Value Object-ის შექმნა და განახლება (ვალიდაცია ხდება აქ)
            if (!string.IsNullOrWhiteSpace(request.Number))
            {
                phoneNumber.Number = Interview.Domain.ValueObjects.PhoneNumber.Create(request.Number);
            }

            // ტელეფონის ნომრის განახლება ბაზაში
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
