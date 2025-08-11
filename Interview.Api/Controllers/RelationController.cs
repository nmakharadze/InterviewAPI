using Interview.Application.Persons.Queries.GetDetail;
using Interview.Application.Persons.Relation.Commands.Create;
using Interview.Application.Persons.Relation.Commands.Delete;
using Interview.Application.Persons.Relation.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Api.Controllers;

/// <summary>
/// ფიზიკურ პირებს შორის კავშირის (Relations) მართვის Controller
/// </summary>
[ApiController]
[Route("api/person")]
public class RelationController : ControllerBase
{
    private readonly IMediator _mediator;

    public RelationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// ფიზიკური პირის კავშირის დამატება
    /// </summary>
    /// <param name="personId">ფიზიკური პირის ID</param>
    /// <param name="request">კავშირის მონაცემები</param>
    /// <returns>დამატებული კავშირი</returns>
    [HttpPost("{personId}/relations")]
    public async Task<ActionResult<RelationDto>> AddRelation(int personId, [FromBody] CreateRelationDto request)
    {
        var command = new CreateRelationCommand
        {
            PersonId = personId,
            RelatedPersonId = request.RelatedPersonId,
            RelationTypeId = request.RelationTypeId
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(AddRelation), new { personId }, result);
    }

    /// <summary>
    /// ფიზიკური პირის კავშირის განახლება
    /// </summary>
    /// <param name="id">კავშირის ID</param>
    /// <param name="request">განახლების მონაცემები</param>
    /// <returns>განახლებული კავშირი</returns>
    [HttpPut("relations/{id}")]
    public async Task<ActionResult<RelationDto>> UpdateRelation(int id, [FromBody] UpdateRelationDto request)
    {
        var command = new UpdateRelationCommand
        {
            Id = id,
            RelatedPersonId = request.RelatedPersonId,
            RelationTypeId = request.RelationTypeId
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// ფიზიკური პირის კავშირის წაშლა
    /// </summary>
    /// <param name="id">კავშირის ID</param>
    /// <returns>წაშლის შედეგი</returns>
    [HttpDelete("relations/{id}")]
    public async Task<ActionResult> DeleteRelation(int id)
    {
        var command = new DeleteRelationCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}


