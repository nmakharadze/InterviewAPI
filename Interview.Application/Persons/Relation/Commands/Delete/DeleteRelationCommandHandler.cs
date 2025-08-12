using System.Threading;
using System.Threading.Tasks;
using Interview.Domain.Entities.Person;
using Interview.Application.Repositories;
using Interview.Application.Repositories.Person;
using MediatR;

namespace Interview.Application.Persons.Relation.Commands.Delete
{
    /// <summary>
    /// Handler კავშირის წაშლისთვის
    /// </summary>
    public class DeleteRelationCommandHandler : IRequestHandler<DeleteRelationCommand, bool>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRelationCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteRelationCommand request, CancellationToken cancellationToken)
        {
            // კავშირის პოვნა
            var relation = await _personRepository.Relations.GetByIdAsync(request.Id);
            if (relation == null)
            {
                return false;
            }

            // კავშირის წაშლა
            await _personRepository.Relations.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
