using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly OpeningTimeOption _option;
    private readonly IProjectService _projectService;
    private readonly IMediator _mediator;
    //public ProjectsController(IOptions<OpeningTimeOption> option)
    //{
    //    _option = option.Value;
    //}

    public ProjectsController(IProjectService projectService, IMediator mediator)
    {
        _projectService = projectService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromBody] string query)
    {
        var getAllProjectsQuery = new GetAllProjectsQuery(query);
        var projects = await _mediator.Send(getAllProjectsQuery);
        
        if (projects is null) return NotFound();
        
        return Ok(projects);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var project = _projectService.GetById(id);
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
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        //quando o command so tem um id, e esse id vem como parâmetro, deve istanciar a classe command
        var command = new DeleteProjectCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id:int}/comments")]
    public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:int}/start")]
    public IActionResult Start(int id)
    {
        _projectService.Start(id);
        return NoContent();
    }

    [HttpPut("{id:int}/finish")]
    public IActionResult Finish(int id)
    {
        _projectService.Finish(id);
        return NoContent();
    }

}