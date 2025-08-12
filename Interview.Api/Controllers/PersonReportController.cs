using Microsoft.AspNetCore.Mvc;
using MediatR;
using Interview.Application.Reports.Person.Queries.GetPersonRelationsReport;

namespace Interview.Api.Controllers;

/// <summary>
/// ფიზიკური პირების რეპორტების კონტროლერი
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PersonReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// ფიზიკური პირების კავშირების რეპორტის მიღება
    /// </summary>
    [HttpGet("person-relations")]
    [ProducesResponseType(typeof(PersonReportDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PersonReportDto>> GetPersonRelationsReport(
        [FromQuery] int? personId,
        [FromQuery] int? relationTypeId)
    {
        var query = new GetPersonRelationsReportQuery
        {
            PersonId = personId,
            RelationTypeId = relationTypeId
        };
        
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
