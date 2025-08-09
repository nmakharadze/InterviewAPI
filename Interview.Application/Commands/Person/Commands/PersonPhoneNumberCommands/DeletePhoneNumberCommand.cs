using MediatR;

namespace Interview.Application.Commands.Person
{
    /// <summary>
    /// ტელეფონის ნომრის წაშლა
    /// </summary>
    public class DeletePhoneNumberCommand : IRequest<bool>
    {
        /// <summary>
        /// ტელეფონის ნომრის ID
        /// </summary>
        public int Id { get; set; }

        public DeletePhoneNumberCommand() { }

        public DeletePhoneNumberCommand(int id)
        {
            Id = id;
        }
    }
}
