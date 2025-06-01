using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica1.Aplication.Queries.PresupuestoQueries;

namespace PruebaTecnica1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PresupuestoController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// POST /api/presupuesto
        /// Body: CreatePresupuestoCommand { UsuarioId, TipoGastoId, Anio, Mes, Monto }
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePresupuestoCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetByUsuarioAndMes), new { usuarioId = cmd.UsuarioId, anio = cmd.Anio, mes = cmd.Mes }, null);
        }

        /// <summary>
        /// GET /api/presupuesto/{usuarioId}/{anio}/{mes}
        /// </summary>
        [HttpGet("{usuarioId:guid}/{anio:int}/{mes:int}")]
        public async Task<IActionResult> GetByUsuarioAndMes(Guid usuarioId, int anio, int mes)
        {
            var query = new GetPresupuestosByUsuarioAndMesQuery
            {
                UsuarioId = usuarioId,
                Anio = anio,
                Mes = mes
            };
            var list = await _mediator.Send(query);
            return Ok(list);
        }
    }
}
