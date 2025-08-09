using Interview.Application.Commands.Person;
using Interview.Application.DTOs.Person;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Api.Controllers;

/// <summary>
/// ტელეფონის ნომრების მართვის Controller
/// </summary>
[ApiController]
[Route("api/person")]
public class PhoneNumberController : ControllerBase
{
    private readonly IMediator _mediator;

    public PhoneNumberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// ფიზიკური პირის ტელეფონის ნომრის დამატება
    /// </summary>
    /// <param name="personId">ფიზიკური პირის ID</param>
    /// <param name="request">ტელეფონის ნომრის მონაცემები</param>
    /// <returns>დამატებული ტელეფონის ნომერი</returns>
    [HttpPost("{personId}/phone-numbers")]
    public async Task<ActionResult<PhoneNumberDto>> AddPhoneNumber(int personId, [FromBody] CreatePhoneNumberDto request)
    {
        var command = new AddPhoneNumberCommand
        {
            PersonId = personId,
            PhoneTypeId = request.PhoneTypeId,
            Number = request.Number
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(AddPhoneNumber), new { personId }, result);
    }

    /// <summary>
    /// ტელეფონის ნომრის განახლება
    /// </summary>
    /// <param name="id">ტელეფონის ნომრის ID</param>
    /// <param name="request">განახლების მონაცემები</param>
    /// <returns>განახლებული ტელეფონის ნომერი</returns>
    [HttpPut("phone-numbers/{id}")]
    public async Task<ActionResult<PhoneNumberDto>> UpdatePhoneNumber(int id, [FromBody] UpdatePhoneNumberDto request)
    {
        var command = new UpdatePhoneNumberCommand
        {
            Id = id,
            PhoneTypeId = request.PhoneTypeId,
            Number = request.Number
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// ტელეფონის ნომრის წაშლა
    /// </summary>
    /// <param name="id">ტელეფონის ნომრის ID</param>
    /// <returns>წაშლის შედეგი</returns>
    [HttpDelete("phone-numbers/{id}")]
    public async Task<ActionResult> DeletePhoneNumber(int id)
    {
        var command = new DeletePhoneNumberCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}


