using MediatR;

namespace Interview.Application.Commands.Person
{
    /// <summary>
    /// კავშირის წაშლა
    /// </summary>
    public class DeleteRelationCommand : IRequest<bool>
    {
        /// <summary>
        /// კავშირის ID
        /// </summary>
        public int Id { get; set; }

        public DeleteRelationCommand() { }

        public DeleteRelationCommand(int id)
        {
            Id = id;
        }
    }
}
