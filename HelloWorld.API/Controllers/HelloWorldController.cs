using HelloWorld.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloWorldController : ControllerBase
{
    private readonly IDatabaseRepository _helloRepo;

    public HelloWorldController(IDatabaseRepository helloRepo)
    {
        _helloRepo = helloRepo;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        var hello = _helloRepo.GetHello();
        return Ok(hello);
    }
}