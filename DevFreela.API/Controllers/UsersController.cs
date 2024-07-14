using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserModel model)
        {
            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        [HttpPut("{id:int}/login")]
        public IActionResult Put(int id, [FromBody] LoginModel model)
        {
            return NoContent();
        }
    }
}
