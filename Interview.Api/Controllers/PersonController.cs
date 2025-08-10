using Microsoft.AspNetCore.Mvc;
using MediatR;
using Interview.Application.DTOs.Person;
using Interview.Application.Queries.Person;
using Interview.Application.Commands.Person;

namespace Interview.Api.Controllers;

/// <summary>
/// ფიზიკური პირების მართვის Controller
/// მოიცავს ყველა CRUD ოპერაციას ფიზიკური პირისთვის
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// ყველა ფიზიკური პირის სიის წამოღება
    /// </summary>
    /// <param name="page">გვერდის ნომერი</param>
    /// <param name="pageSize">გვერდზე ჩანაწერების რაოდენობა</param>
    /// <returns>ფიზიკური პირების სია</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonListDto>>> GetAll(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10)
    {
        var query = new GetAllPersonsQuery { Page = page, PageSize = pageSize };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// ფიზიკური პირის წამოღება ID-ით
    /// </summary>
    /// <param name="id">ფიზიკური პირის ID</param>
    /// <returns>ფიზიკური პირის ინფორმაცია</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetById(int id)
    {
        var query = new GetPersonQuery { Id = id };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// ფიზიკური პირის სრული ინფორმაციის მიღება ID-ით
    /// </summary>
    /// <param name="id">ფიზიკური პირის ID</param>
    /// <returns>ფიზიკური პირის სრული ინფორმაცია ტელეფონის ნომრებით და ურთიერთობებით</returns>
    [HttpGet("{id}/details")]
    public async Task<ActionResult<PersonDetailDto>> GetDetails(int id)
    {
        var query = new GetPersonDetailQuery { Id = id };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// ფიზიკური პირის ძებნა
    /// </summary>
    /// <param name="searchTerm">ძებნის ინფორმაცია</param>
    /// <param name="cityId">ქალაქის ID</param>
    /// <param name="genderId">სქესის ID</param>
    /// <param name="page">გვერდის ნომერი</param>
    /// <param name="pageSize">გვერდზე ჩანაწერების რაოდენობა</param>
    /// <returns>ძებნის შედეგები</returns>
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<PersonListDto>>> Search(
        [FromQuery] string? searchTerm,
        [FromQuery] int? cityId,
        [FromQuery] int? genderId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new SearchPersonsQuery 
        { 
            SearchTerm = searchTerm,
            CityId = cityId,
            GenderId = genderId,
            Page = page,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// ახალი ფიზიკური პირის შექმნა
    /// </summary>
    /// <param name="request">ფიზიკური პირის შექმნის მონაცემები</param>
    /// <returns>შექმნილი ფიზიკური პირის ინფორმაცია</returns>
    [HttpPost]
    public async Task<ActionResult<PersonDto>> Create([FromBody] CreatePersonDto request)
    {
        var command = new CreatePersonCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            GenderId = request.GenderId,
            PersonalNumber = request.PersonalNumber,
            BirthDate = request.BirthDate,
            CityId = request.CityId,
            ImagePath = request.ImagePath,
            PhoneNumbers = request.PhoneNumbers?.Select(pn => new CreatePhoneNumberDto
            {
                PhoneTypeId = pn.PhoneTypeId,
                Number = pn.Number
            }).ToList(),
            Relations = request.Relations?.Select(r => new CreateRelationDto
            {
                RelatedPersonId = r.RelatedPersonId,
                RelationTypeId = r.RelationTypeId
            }).ToList()
        };
        
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, result);
    }

    /// <summary>
    /// ფიზიკური პირის განახლება
    /// </summary>
    /// <param name="id">ფიზიკური პირის ID</param>
    /// <param name="request">განახლების მონაცემები</param>
    /// <returns>განახლებული ფიზიკური პირის ინფორმაცია</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<PersonDto>> Update(int id, [FromBody] UpdatePersonDto request)
    {
        var command = new UpdatePersonCommand
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            GenderId = request.GenderId,
            PersonalNumber = request.PersonalNumber,
            BirthDate = request.BirthDate,
            CityId = request.CityId
        };
        
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// ფიზიკური პირის წაშლა
    /// </summary>
    /// <param name="id">ფიზიკური პირის ID</param>
    /// <returns>წაშლის შედეგი</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeletePersonCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    
}
