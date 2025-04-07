using Microsoft.AspNetCore.Mvc;

namespace TodoList.WebApi.Controllers;

[ApiController]
[Route("[controller]/api")]
public class TestController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Hello World!");
    }
}