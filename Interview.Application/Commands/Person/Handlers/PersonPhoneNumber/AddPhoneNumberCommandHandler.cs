using System;
using System.Threading;
using System.Threading.Tasks;
using Interview.Application.DTOs.Person;
using Interview.Domain.Entities.Person;
using Interview.Domain.Entities.Dictionary;
using Interview.Domain.ValueObjects;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Commands.Person.Handlers
{
    /// <summary>
    /// Handler ტელეფონის ნომრის დამატებისთვის
    /// </summary>
    public class AddPhoneNumberCommandHandler : IRequestHandler<AddPhoneNumberCommand, int>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PhoneType> _phoneTypeRepository;

        public AddPhoneNumberCommandHandler(
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork,
            IGenericRepository<PhoneType> phoneTypeRepository)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _phoneTypeRepository = phoneTypeRepository;
        }

        public async Task<int> Handle(AddPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            // პირის ვალიდაცია
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person == null)
                throw new ArgumentException($"ფიზიკური პირი ID {request.PersonId} არ არსებობს");

            // ტელეფონის ნომრის ტიპის ვალიდაცია
            var phoneTypeExists = await _phoneTypeRepository.ExistsAsync(request.PhoneTypeId);
            if (!phoneTypeExists)
                throw new ArgumentException($"ტელეფონის ნომრის ტიპი ID {request.PhoneTypeId} არ არსებობს");

            // ტელეფონის ნომრის ვალიდაცია
            var phoneNumber = PhoneNumber.Create(request.Number);

            // ტელეფონის ნომრის შექმნა
            var personPhone = new PersonPhoneNumber
            {
                PersonId = request.PersonId,
                PhoneTypeId = request.PhoneTypeId,
                Number = phoneNumber
            };

            // ტელეფონის ნომრის დამატება
            await _personRepository.AddPhoneNumberAsync(personPhone);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return personPhone.Id;
        }
    }
}
