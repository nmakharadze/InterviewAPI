using MediatR;
using Microsoft.AspNetCore.Http;

namespace Interview.Application.Persons.Image.Commands.UploadImage
{
    public class UploadOrUpdateImageCommand : IRequest<string>
    {
        public int PersonId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
