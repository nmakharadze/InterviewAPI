using MediatR;

namespace Interview.Application.Persons.Image.Commands.DeleteImage
{
    public class DeleteImageCommand : IRequest<bool>
    {
        public int PersonId { get; set; }
    }
}
