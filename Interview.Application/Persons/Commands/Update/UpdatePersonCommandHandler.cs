using System;
using System.Threading;
using System.Threading.Tasks;
using Interview.Application.Persons.Commands.Update;
using Interview.Domain.Entities.Person;
using Interview.Domain.Entities.Dictionary;
using Interview.Domain.ValueObjects;
using Interview.Application.Repositories;
using Interview.Application.Repositories.Person;
using MediatR;

namespace Interview.Application.Persons.Commands.Update
{
    /// <summary>
    /// Handler ფიზიკური პირის განახლებისთვის
    /// </summary>
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Gender> _genderRepository;
        private readonly IGenericRepository<City> _cityRepository;

        public UpdatePersonCommandHandler(
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork,
            IGenericRepository<Gender> genderRepository,
            IGenericRepository<City> cityRepository)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _genderRepository = genderRepository;
            _cityRepository = cityRepository;
        }

        public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            // ფიზიკური პირის პოვნა
            var person = await _personRepository.GetByIdAsync(request.Id);
            if (person == null)
            {
                return false;
            }

            // ვალიდაცია - ვამოწმებთ რომ ცნობარები არსებობს
            await ValidateReferencesAsync(request, cancellationToken);

            // Value Objects-ის შექმნა და განახლება (ვალიდაცია ხდება აქ)
            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                person.FirstName = Name.Create(request.FirstName);
            }

            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                person.LastName = Name.Create(request.LastName);
            }

            if (!string.IsNullOrWhiteSpace(request.PersonalNumber))
            {
                person.PersonalNumber = PersonalNumber.Create(request.PersonalNumber);
            }

            if (request.BirthDate.HasValue)
            {
                person.BirthDate = BirthDate.Create(request.BirthDate.Value);
            }

            if (request.GenderId.HasValue)
            {
                person.GenderId = request.GenderId.Value;
            }

            if (request.CityId.HasValue)
            {
                person.CityId = request.CityId.Value;
            }

            // ფიზიკური პირის განახლება ბაზაში
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task ValidateReferencesAsync(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            // სქესის ვალიდაცია
            if (request.GenderId.HasValue)
            {
                var genderExists = await _genderRepository.ExistsAsync(request.GenderId.Value);
                if (!genderExists)
                    throw new ArgumentException($"სქესი ID {request.GenderId.Value} არ არსებობს");
            }

            // ქალაქის ვალიდაცია
            if (request.CityId.HasValue)
            {
                var cityExists = await _cityRepository.ExistsAsync(request.CityId.Value);
                if (!cityExists)
                    throw new ArgumentException($"ქალაქი ID {request.CityId.Value} არ არსებობს");
            }
        }
    }
}
