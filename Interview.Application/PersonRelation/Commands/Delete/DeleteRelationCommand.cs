using MediatR;

namespace Interview.Application.PersonRelation.Commands.Delete
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
