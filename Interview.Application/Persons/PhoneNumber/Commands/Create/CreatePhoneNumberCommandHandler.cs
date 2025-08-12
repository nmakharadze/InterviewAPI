using System;
using System.Threading;
using System.Threading.Tasks;
using Interview.Domain.Entities.Person;
using Interview.Domain.Entities.Dictionary;
using Interview.Domain.ValueObjects;
using Interview.Application.Repositories;
using Interview.Application.Repositories.Person;
using MediatR;

namespace Interview.Application.Persons.PhoneNumber.Commands.Create
{
    /// <summary>
    /// Handler ტელეფონის ნომრის დამატებისთვის
    /// </summary>
    public class CreatePhoneNumberCommandHandler : IRequestHandler<CreatePhoneNumberCommand, int>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PhoneType> _phoneTypeRepository;

        public CreatePhoneNumberCommandHandler(
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork,
            IGenericRepository<PhoneType> phoneTypeRepository)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _phoneTypeRepository = phoneTypeRepository;
        }

        public async Task<int> Handle(CreatePhoneNumberCommand request, CancellationToken cancellationToken)
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
            var phoneNumber = Interview.Domain.ValueObjects.PhoneNumber.Create(request.Number);

            // ტელეფონის ნომრის შექმნა
            var personPhone = new Domain.Entities.Person.PersonPhoneNumber
            {
                PersonId = request.PersonId,
                PhoneTypeId = request.PhoneTypeId,
                Number = phoneNumber
            };

            // ტელეფონის ნომრის დამატება
            await _personRepository.Phones.AddAsync(personPhone);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return personPhone.Id;
        }
    }
}
