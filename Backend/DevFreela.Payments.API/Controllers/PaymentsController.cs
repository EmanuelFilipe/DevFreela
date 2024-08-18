using DevFreela.Payments.API.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Payments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result) return BadRequest("O pagamento não pode ser processado.");

            return NoContent();
        }
    }
}
