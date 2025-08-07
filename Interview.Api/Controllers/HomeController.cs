using Microsoft.AspNetCore.Mvc;

namespace Interview.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("it works");
    }
}
