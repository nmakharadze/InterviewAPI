using MediatR;
using Microsoft.AspNetCore.Mvc;
using Interview.Application.Persons.Image.Commands.UploadImage;
using Interview.Application.Persons.Image.Commands.DeleteImage;
using Interview.Application.Services;
using Interview.Application.Helpers;

namespace Interview.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileStorageService _fileStorageService;

        public ImageController(IMediator mediator, IFileStorageService fileStorageService)
        {
            _mediator = mediator;
            _fileStorageService = fileStorageService;
        }

        /// <summary>
        /// ფიზიკური პირის ფოტოს ატვირთვა ან განახლება
        /// </summary>
        [HttpPost("upload-or-update/{personId}")]
        public async Task<ActionResult<string>> UploadOrUpdateImage(int personId, IFormFile file)
        {
            var command = new UploadOrUpdateImageCommand 
            { 
                PersonId = personId, 
                ImageFile = file 
            };
            
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// ფიზიკური პირის ფოტოს წაშლა
        /// </summary>
        [HttpDelete("{personId}")]
        public async Task<ActionResult> DeleteImage(int personId)
        {
            var command = new DeleteImageCommand { PersonId = personId };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// ფიზიკური პირის ფოტოს მიღება ან გადმოწერა
        /// </summary>
        /// <param name="personId">ფიზიკური პირის ID</param>
        /// <param name="download">true - ფაილის გადმოწერა, false - ბრაუზერში ჩვენება</param>
        [HttpGet("{personId}")]
        public ActionResult GetImage(int personId, [FromQuery] bool download = false)
        {
            var result = _fileStorageService.GetFileWithContentType(personId);
            
            if (result == null)
                return NotFound("ფიზიკური პირის ფოტო ვერ მოიძებნა");
            
            var response = File(result.Value.Stream, result.Value.ContentType);
            
            if (download)
            {
                // ფაილის გადმოწერისთვის
                var fileName = $"{personId}_profile{FileExtensionHelper.GetFileExtension(result.Value.ContentType)}";
                response.FileDownloadName = fileName;
            }
            
            return response;
        }
    }
}
