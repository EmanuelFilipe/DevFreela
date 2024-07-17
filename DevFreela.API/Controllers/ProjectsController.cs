using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly OpeningTimeOption _option;
    private readonly IProjectService _projectService;
    //public ProjectsController(IOptions<OpeningTimeOption> option)
    //{
    //    _option = option.Value;
    //}

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public IActionResult Get([FromBody] string query)
    {
        var projects = _projectService.GetAll(query);
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
    public IActionResult Post([FromBody] CreateProjectInputModel inputModel)
    {
        int id = _projectService.Create(inputModel);
        return CreatedAtAction(nameof(GetById), new { id }, inputModel);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] UpdateProjectInputModel inputModel)
    {
        _projectService.Update(inputModel);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _projectService.Delete(id);
        return NoContent();
    }

    [HttpPost("{id:int}/comments")]
    public IActionResult PostComment(int id, [FromBody] CreateCommentProjectInputModel inputModel)
    {
        _projectService.CreateComment(inputModel);
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