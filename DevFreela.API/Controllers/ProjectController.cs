using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        [HttpGet(Name = "Teste")]
        public IActionResult Teste()
        {
            return Ok();
        }
    }
}
