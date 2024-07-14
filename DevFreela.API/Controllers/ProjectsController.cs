using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly OpeningTimeOption _option;

    public ProjectsController(IOptions<OpeningTimeOption> option)
    {
        _option = option.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {

        return Ok();
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        return Ok(id);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateProjectModel model)
    {
        return Ok();
        //return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] UpdateProjectModel model)
    {
        return Ok();
        //return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return Ok();
        //return NoContent();
    }

    [HttpPost("{id:int}/comments")]
    public IActionResult PostComment(int id, [FromBody] CreateCommentModel model)
    {
        return NoContent();
    }

    [HttpPut("{id:int}/start")]
    public IActionResult Start(int id)
    {
        return NoContent();
    }

    [HttpPut("{id:int}/finish")]
    public IActionResult Finish(int id)
    {
        return NoContent();
    }

}