using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Interview.Application.Persons.Commands.Create;
using Interview.Domain.Entities.Person;
using Interview.Domain.Entities.Dictionary;
using Interview.Domain.ValueObjects;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Persons.Commands.Create
{
    /// <summary>
    /// Handler ფიზიკური პირის შექმნა სრული ინფორმაციით
    /// </summary>
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Gender> _genderRepository;
        private readonly IGenericRepository<City> _cityRepository;
        private readonly IGenericRepository<PhoneType> _phoneTypeRepository;
        private readonly IGenericRepository<RelationType> _relationTypeRepository;

        public CreatePersonCommandHandler(
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork,
            IGenericRepository<Gender> genderRepository,
            IGenericRepository<City> cityRepository,
            IGenericRepository<PhoneType> phoneTypeRepository,
            IGenericRepository<RelationType> relationTypeRepository)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _genderRepository = genderRepository;
            _cityRepository = cityRepository;
            _phoneTypeRepository = phoneTypeRepository;
            _relationTypeRepository = relationTypeRepository;
        }

        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            // ვალიდაცია - მოწმდება ცნობარის არსებობა
            await ValidateReferencesAsync(request, cancellationToken);

            // Value Objects-ის შექმნა (ვალიდაცია ხდება აქ)
            var firstName = Name.Create(request.FirstName);
            var lastName = Name.Create(request.LastName);
            var personalNumber = PersonalNumber.Create(request.PersonalNumber);
            var birthDate = BirthDate.Create(request.BirthDate);

            // ფიზიკური პირის შექმნა
            var person = new Interview.Domain.Entities.Person.Person
            {
                FirstName = firstName,
                LastName = lastName,
                GenderId = request.GenderId,
                PersonalNumber = personalNumber,
                BirthDate = birthDate,
                CityId = request.CityId,
                ImagePath = request.ImagePath
            };

            // ტელეფონის ნომრების დამატება
            if (request.PhoneNumbers != null && request.PhoneNumbers.Count > 0)
            {
                foreach (var phoneDto in request.PhoneNumbers)
                {
                    var phoneNumber = PhoneNumber.Create(phoneDto.Number);
                    var personPhone = new Interview.Domain.Entities.Person.PersonPhoneNumber
                    {
                        PhoneTypeId = phoneDto.PhoneTypeId,
                        Number = phoneNumber
                    };
                    person.PhoneNumbers.Add(personPhone);
                }
            }

            // კავშირების დამატება
            if (request.Relations != null && request.Relations.Count > 0)
            {
                foreach (var relationDto in request.Relations)
                {
                    var personRelation = new Interview.Domain.Entities.Person.PersonRelation
                    {
                        RelatedPersonId = relationDto.RelatedPersonId,
                        RelationTypeId = relationDto.RelationTypeId
                    };
                    person.Relations.Add(personRelation);
                }
            }

            // ფიზიკური პირის შენახვა ბაზაში
            await _personRepository.AddAsync(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return person.Id;
        }

        private async Task ValidateReferencesAsync(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            // სქესის ვალიდაცია
            var genderExists = await _genderRepository.ExistsAsync(request.GenderId);
            if (!genderExists)
                throw new ArgumentException($"სქესი ID {request.GenderId} არ არსებობს");

            // ქალაქის ვალიდაცია
            var cityExists = await _cityRepository.ExistsAsync(request.CityId);
            if (!cityExists)
                throw new ArgumentException($"ქალაქი ID {request.CityId} არ არსებობს");

            // ტელეფონის ნომრების ტიპების ვალიდაცია
            if (request.PhoneNumbers != null)
            {
                foreach (var phoneDto in request.PhoneNumbers)
                {
                    var phoneTypeExists = await _phoneTypeRepository.ExistsAsync(phoneDto.PhoneTypeId);
                    if (!phoneTypeExists)
                        throw new ArgumentException($"ტელეფონის ნომრის ტიპი ID {phoneDto.PhoneTypeId} არ არსებობს");
                }
            }

            // კავშირების ტიპების ვალიდაცია
            if (request.Relations != null)
            {
                foreach (var relationDto in request.Relations)
                {
                    var relationTypeExists = await _relationTypeRepository.ExistsAsync(relationDto.RelationTypeId);
                    if (!relationTypeExists)
                        throw new ArgumentException($"კავშირის ტიპი ID {relationDto.RelationTypeId} არ არსებობს");

                    // დაკავშირებული ფიზიკური პირის ვალიდაცია
                    var relatedPersonExists = await _personRepository.ExistsAsync(relationDto.RelatedPersonId);
                    if (!relatedPersonExists)
                        throw new ArgumentException($"დაკავშირებული ფიზიკური პირი ID {relationDto.RelatedPersonId} არ არსებობს");
                }
            }
        }
    }
}
