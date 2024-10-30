using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.Project.DeleteProject;
using DevFreela.Application.Commands.Project.CreateProject;
using DevFreela.Application.Commands.Project.FinishProject;
using DevFreela.Application.Commands.Project.UpdateProject;
using DevFreela.Application.Commands.Project.StartProject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Queries.Projects.GetAllProjects;
using DevFreela.Application.Queries.Projects.GetProjectsGrouped;
using DevFreela.Application.Queries.Projects.GetProjectById;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "admin, client")]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllProjectsQuery getAllProjectsQuery)
    {
        var projects = await _mediator.Send(getAllProjectsQuery);
        
        if (projects is null) return NotFound();
        
        return Ok(projects);
    }

    [HttpGet("projects-grouped")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetProjectsGrouped()
    {
        var query = new GetProjectsGroupedQuery();
        var projectsGrouped = await _mediator.Send(query);

        if (projectsGrouped is null) return NotFound();

        return Ok(projectsGrouped);
    }

    [Authorize(Roles = "admin, client")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var getProjectByIdQuery = new GetProjectByIdQuery(id);
        var project = await _mediator.Send(getProjectByIdQuery);

        if (project is null) return NotFound();

        return Ok(project);
    }

    [HttpPost]
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
    [Authorize(Roles = "admin, client")]
    public async Task<IActionResult> Delete(int id)
    {
        //quando o command so tem um id, e esse id vem como parâmetro, deve istanciar a classe command
        var command = new DeleteProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:int}/comments")]
    [Authorize(Roles = "admin, client, freelancer")]
    public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:int}/start")]
    [Authorize(Roles = "admin, client")]
    public async Task<IActionResult> Start(int id)
    {
        var command = new StartProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:int}/finish")]
    [Authorize(Roles = "admin, client")]
    public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command)
    {
        command.Id = id;

        var result = await _mediator.Send(command);

        if (!result) return BadRequest("O pagamento não pode ser processado.");

        return Accepted();
    }

}