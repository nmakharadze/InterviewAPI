using System.Threading;
using System.Threading.Tasks;
using Interview.Domain.Entities.Person;
using Interview.Application.Repositories;
using MediatR;

namespace Interview.Application.Commands.Person.Handlers
{
    /// <summary>
    /// Handler ტელეფონის ნომრის წაშლისთვის
    /// </summary>
    public class DeletePhoneNumberCommandHandler : IRequestHandler<DeletePhoneNumberCommand, bool>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePhoneNumberCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            // ტელეფონის ნომრის პოვნა
            var phoneNumber = await _personRepository.GetPhoneNumberByIdAsync(request.Id);
            if (phoneNumber == null)
            {
                return false;
            }

            // ტელეფონის ნომრის წაშლა
            await _personRepository.DeletePhoneNumberAsync(request.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
