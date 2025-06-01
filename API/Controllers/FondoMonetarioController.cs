using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica1.Aplication.Commands.FondoMonetarioCommand;
using PruebaTecnica1.Aplication.Queries.FondoMonetarioQueries;

namespace PruebaTecnica1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FondoMonetarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FondoMonetarioController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// POST /api/fondomonetario
        /// Body: CreateFondoMonetarioCommand { Nombre, Tipo, SaldoInicial }
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFondoMonetarioCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        /// <summary>
        /// GET /api/fondomonetario
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _mediator.Send(new GetAllFondosQuery());
            return Ok(list);
        }

        /// <summary>
        /// GET /api/fondomonetario/{id}
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _mediator.Send(new GetFondoByIdQuery { Id = id });
            return Ok(dto);
        }

        /// <summary>
        /// PUT /api/fondomonetario/{id}
        /// Body: UpdateFondoMonetarioCommand { Id, Nombre, Tipo }
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFondoMonetarioCommand cmd)
        {
            if (id != cmd.Id) return BadRequest("El Id en URL y en cuerpo no coinciden.");
            await _mediator.Send(cmd);
            return NoContent();
        }

        /// <summary>
        /// DELETE /api/fondomonetario/{id}
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteFondoMonetarioCommand(id));
            return NoContent();
        }
    }
}
