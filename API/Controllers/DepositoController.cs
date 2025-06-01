using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica1.Aplication.Commands.DepositoCommnad;

namespace PruebaTecnica1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepositoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepositoController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// POST /api/deposito
        /// Body: CreateDepositoCommand { UsuarioId, FondoMonetarioId, Fecha, Monto }
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepositoCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(null, new { id }, null);
            // Aun no hay GetById específico, pero podríamos crearlo
        }
    }
}
