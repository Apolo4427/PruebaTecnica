using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica1.Aplication.Commands.RegistroGastoCommand;

namespace PruebaTecnica1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroGastoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RegistroGastoController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// POST /api/registrogasto
        /// Body: CreateRegistroGastoCommand {
        ///    UsuarioId, FondoMonetarioId, Fecha, NombreComercio, TipoDocumento, Observaciones,
        ///    List<(Guid TipoGastoId, decimal Monto)> Detalles
        /// }
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegistroGastoCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(null, new { id }, null);
            // No exponemos GetById explícito para registro de gasto, 
            // pero podríamos agregar un Get para mostrar detalles.
        }
    }
}
