using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica1.Aplication.Queries.Comparativo;
using PruebaTecnica1.Aplication.Queries.MovimientosQueries;

namespace PruebaTecnica1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MovimientosController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// GET /api/movimientos/{usuarioId}?desde=yyyy-MM-dd&hasta=yyyy-MM-dd
        /// </summary>
        [HttpGet("{usuarioId:guid}")]
        public async Task<IActionResult> GetByRango(
            Guid usuarioId,
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            var query = new GetMovimientosPorRangoQuery
            {
                UsuarioId = usuarioId,
                Desde = desde,
                Hasta = hasta
            };
            var list = await _mediator.Send(query);
            return Ok(list);
        }

        /// <summary>
        /// GET /api/movimientos/comparativo/{usuarioId}?desde=yyyy-MM-dd&hasta=yyyy-MM-dd
        /// </summary>
        [HttpGet("comparativo/{usuarioId:guid}")]
        public async Task<IActionResult> GetComparativo(
            Guid usuarioId,
            [FromQuery] DateTime desde,
            [FromQuery] DateTime hasta)
        {
            var query = new GetComparativoPresupuestoEjecucionQuery
            {
                UsuarioId = usuarioId,
                Desde = desde,
                Hasta = hasta
            };
            var list = await _mediator.Send(query);
            return Ok(list);
        }
    }
}
