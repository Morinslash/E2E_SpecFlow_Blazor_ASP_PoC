using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok("Hello World");
    }
}