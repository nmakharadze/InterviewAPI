using Interview.Application.Dictionaries.Commands.Create;
using Interview.Application.Dictionaries.Commands.Delete;
using Interview.Application.Dictionaries.Commands.Update;
using Interview.Application.Dictionaries.Queries.GetAll;
using Interview.Application.Dictionaries.Queries.GetDetail;
using Interview.Application.Dictionaries.Queries.GetDictionaryTable;
using Interview.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Interview.Api.Controllers;

/// <summary>
/// DictionaryController - დინამიური controller ყველა dictionary სქემის ცხრილისთვის
/// გამოიყენება ყველა dictionary სქემის ცხრილისთვის CRUD ოპერაციების შესასრულებლად
/// </summary>
[ApiController]
[Route("api/dictionary")]
public class DictionaryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IDictionaryRepository _dictionaryRepository;

    public DictionaryController(IMediator mediator, IDictionaryRepository dictionaryRepository)
    {
        _mediator = mediator;
        _dictionaryRepository = dictionaryRepository;
    }

    /// <summary>
    /// dictionary სქემის ცხრილიდან ყველა ჩანაწერის მიღება ცხრილის დასახელების მიხედვით
    /// </summary>
    /// <param name="tableName">ცხრილის დასახელება მაგ: cities, genders, phonetypes, relationtypes</param>
    /// <returns>მოთხოვნილი ცხრილის ჩანაწერების სია</returns>
    [HttpGet("{tableName}")]
    public async Task<ActionResult<IEnumerable<DictionaryListDto>>> GetAll(string tableName)
    {
        await _dictionaryRepository.ValidateDictionaryTableAsync(tableName);
        
        var query = new GetAllDictionariesQuery { DictionaryTableName = tableName };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// კონკრეტული dictionary სქემის ცხრილიდან ჩანაწერის მიღება ID-ის მიხედვით
    /// </summary>
    /// <param name="tableName">ცხრილის დასახელება</param>
    /// <param name="id">ჩანაწერის ID</param>
    /// <returns>Dictionary ჩანაწერი</returns>
    [HttpGet("{tableName}/{id}")]
    public async Task<ActionResult<DictionaryDetailDto>> GetById(string tableName, int id)
    {
        await _dictionaryRepository.ValidateDictionaryTableAsync(tableName);
        
        var query = new GetDictionaryQuery { DictionaryTableName = tableName, Id = id };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// ახალი ჩანაწერის შექმნა dictionary სქემის ცხრილში 
    /// </summary>
    /// <param name="tableName">ცხრილის დასახელება</param>
    /// <param name="dto">შექმნის მონაცემები</param>
    /// <returns>შექმნილი ჩანაწერი</returns>
    [HttpPost("{tableName}")]
    public async Task<ActionResult<CreateDictionaryResultDto>> Create(string tableName, [FromBody] CreateDictionaryDto dto)
    {
        await _dictionaryRepository.ValidateDictionaryTableAsync(tableName);
        
        var command = new CreateDictionaryCommand 
        { 
            DictionaryTableName = tableName, 
            Name = dto.Name 
        };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { tableName, id = result.Id }, result);
    }

    /// <summary>
    /// dictionary სქემის ცხრილში არსებული ჩანაწერის განახლება
    /// </summary>
    /// <param name="tableName">ცხრილის დასახელება</param>
    /// <param name="id">ჩანაწერის ID</param>
    /// <param name="dto">განსაახლებელი მონაცემები</param>
    /// <returns>განახლებული ჩანაწერი</returns>
    [HttpPut("{tableName}/{id}")]
    public async Task<ActionResult<UpdateDictionaryResultDto>> Update(string tableName, int id, [FromBody] UpdateDictionaryDto dto)
    {
        await _dictionaryRepository.ValidateDictionaryTableAsync(tableName);
        
        var command = new UpdateDictionaryCommand 
        { 
            DictionaryTableName = tableName, 
            Id = id, 
            Name = dto.Name 
        };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// dictionary სქემის ცხრილიდან ჩანაწერის წაშლა
    /// </summary>
    /// <param name="tableName">ცხრილის დასახელება</param>
    /// <param name="id">ჩანაწერის ID</param>
    /// <returns>წაშლის შედეგი</returns>
    [HttpDelete("{tableName}/{id}")]
    public async Task<ActionResult<bool>> Delete(string tableName, int id)
    {
        await _dictionaryRepository.ValidateDictionaryTableAsync(tableName);
        
        var command = new DeleteDictionaryCommand { DictionaryTableName = tableName, Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// ყველა dictionary სქემის ცხრილის დასახელების მიღება
    /// </summary>
    /// <returns>Dictionary სქემის ცხრილების დასახელებების სია</returns>
    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<DictionaryTableDto>>> GetDictionaryTypes()
    {
        var query = new GetDictionaryTableQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
