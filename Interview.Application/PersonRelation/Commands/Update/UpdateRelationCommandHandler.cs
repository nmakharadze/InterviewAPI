using System;
using System.Threading;
using System.Threading.Tasks;
using Interview.Application.PersonRelation.Commands.Update;
using Interview.Domain.Entities.Person;
using Interview.Domain.Entities.Dictionary;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.PersonRelation.Commands.Update
{
    /// <summary>
    /// Handler კავშირის განახლებისთვის
    /// </summary>
    public class UpdateRelationCommandHandler : IRequestHandler<UpdateRelationCommand, bool>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<RelationType> _relationTypeRepository;

        public UpdateRelationCommandHandler(
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork,
            IGenericRepository<RelationType> relationTypeRepository)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _relationTypeRepository = relationTypeRepository;
        }

        public async Task<bool> Handle(UpdateRelationCommand request, CancellationToken cancellationToken)
        {
            // კავშირის პოვნა
            var relation = await _personRepository.GetRelationByIdAsync(request.Id);
            if (relation == null)
            {
                return false;
            }

            // დაკავშირებული ფიზიკური პირის ვალიდაცია
            if (request.RelatedPersonId.HasValue)
            {
                var relatedPersonExists = await _personRepository.ExistsAsync(request.RelatedPersonId.Value);
                if (!relatedPersonExists)
                {
                    throw new ArgumentException($"დაკავშირებული ფიზიკური პირი ID {request.RelatedPersonId.Value} არ არსებობს");
                }

                // თავის თავზე კავშირის შემოწმება
                if (relation.PersonId == request.RelatedPersonId.Value)
                {
                    throw new ArgumentException("ფიზიკური პირი არ შეიძლება იყოს დაკავშირებული თავის თავთან");
                }

                relation.RelatedPersonId = request.RelatedPersonId.Value;
            }

            // კავშირის ტიპის ვალიდაცია
            if (request.RelationTypeId.HasValue)
            {
                var relationTypeExists = await _relationTypeRepository.ExistsAsync(request.RelationTypeId.Value);
                if (!relationTypeExists)
                {
                    throw new ArgumentException($"კავშირის ტიპი ID {request.RelationTypeId.Value} არ არსებობს");
                }
                relation.RelationTypeId = request.RelationTypeId.Value;
            }

            // კავშირის განახლება ბაზაში
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
