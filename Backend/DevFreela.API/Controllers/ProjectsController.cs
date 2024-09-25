using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.Project.DeleteProject;
using DevFreela.Application.Commands.Project.CreateProject;
using DevFreela.Application.Commands.Project.FinishProject;
using DevFreela.Application.Commands.Project.UpdateProject;
using DevFreela.Application.Commands.Project.StartProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class ProjectsController : ControllerBase
{
    //private readonly OpeningTimeOption _option;
    private readonly IMediator _mediator;
    //public ProjectsController(IOptions<OpeningTimeOption> option)
    //{
    //    _option = option.Value;
    //}

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    //[Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> Get([FromQuery] GetAllProjectsQuery getAllProjectsQuery)
    {
        var projects = await _mediator.Send(getAllProjectsQuery);
        
        if (projects is null) return NotFound();
        
        return Ok(projects);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var getProjectByIdQuery = new GetProjectByIdQuery(id);
        var project = await _mediator.Send(getProjectByIdQuery);

        if (project is null) return NotFound();

        return Ok(project);
    }

    [HttpPost]
    //[Authorize(Roles = "client")]
    public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
    {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    [HttpPut("{id:int}")]
    //[Authorize(Roles = "client")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Delete(int id)
    {
        //quando o command so tem um id, e esse id vem como parâmetro, deve istanciar a classe command
        var command = new DeleteProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:int}/comments")]
    [Authorize(Roles = "client, freelancer")]
    public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:int}/start")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Start(int id)
    {
        var command = new StartProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:int}/finish")]
    [Authorize(Roles = "client")]
    public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command)
    {
        command.Id = id;

        var result = await _mediator.Send(command);

        if (!result) return BadRequest("O pagamento não pode ser processado.");

        return Accepted();
    }

}