using System;
using System.Threading;
using System.Threading.Tasks;
using Interview.Domain.Entities.Person;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Commands.Person.Handlers
{
    /// <summary>
    /// Handler ფიზიკური პირის წაშლისთვის
    /// </summary>
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            // ფიზიკური პირის პოვნა
            var person = await _personRepository.GetByIdAsync(request.Id);
            if (person == null)
            {
                return false;
            }

            // ფიზიკური პირის წაშლა (Cascade Delete-ით წაიშლება ყველა დაკავშირებული მონაცემი)
            await _personRepository.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
