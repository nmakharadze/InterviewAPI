using System;
using System.Threading;
using System.Threading.Tasks;
using Interview.Domain.Entities.Person;
using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using Interview.Application.Repositories.Person;
using MediatR;

namespace Interview.Application.Persons.Relation.Commands.Create
{
    /// <summary>
    /// Handler კავშირის დამატებისთვის
    /// </summary>
    public class CreateRelationCommandHandler : IRequestHandler<CreateRelationCommand, int>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<RelationType> _relationTypeRepository;

        public CreateRelationCommandHandler(
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork,
            IGenericRepository<RelationType> relationTypeRepository)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _relationTypeRepository = relationTypeRepository;
        }

        public async Task<int> Handle(CreateRelationCommand request, CancellationToken cancellationToken)
        {
            // ფიზიკური პირის ვალიდაცია
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person == null)
                throw new ArgumentException($"ფიზიკური პირი ID {request.PersonId} არ არსებობს");

            // დაკავშირებული ფიზიკური პირის ვალიდაცია
            var relatedPersonExists = await _personRepository.ExistsAsync(request.RelatedPersonId);
            if (!relatedPersonExists)
                throw new ArgumentException($"დაკავშირებული ფიზიკური პირი ID {request.RelatedPersonId} არ არსებობს");

            // კავშირის ტიპის ვალიდაცია
            var relationTypeExists = await _relationTypeRepository.ExistsAsync(request.RelationTypeId);
            if (!relationTypeExists)
                throw new ArgumentException($"კავშირის ტიპი ID {request.RelationTypeId} არ არსებობს");

            // თავის თავზე კავშირის შემოწმება
            if (request.PersonId == request.RelatedPersonId)
                throw new ArgumentException("ფიზიკური პირი არ შეიძლება იყოს დაკავშირებული თავის თავთან");

            // კავშირის შექმნა
            var personRelation = new Interview.Domain.Entities.Person.PersonRelation
            {
                PersonId = request.PersonId,
                RelatedPersonId = request.RelatedPersonId,
                RelationTypeId = request.RelationTypeId
            };

            // კავშირის დამატება
            await _personRepository.Relations.AddAsync(personRelation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return personRelation.Id;
        }
    }
}
