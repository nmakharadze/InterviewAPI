using MediatR;

namespace Interview.Application.Commands.Person
{
    /// <summary>
    /// ფიზიკური პირის წაშლა
    /// </summary>
    public class DeletePersonCommand : IRequest<bool>
    {
        /// <summary>
        /// ფიზიკური პირის ID
        /// </summary>
        public int Id { get; set; }

        public DeletePersonCommand() { }

        public DeletePersonCommand(int id)
        {
            Id = id;
        }
    }
}
