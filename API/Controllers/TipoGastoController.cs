// API/Controllers/TipoGastoController.cs
using System.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica1.Aplication.Commands.TipoGastoCommand;
using PruebaTecnica1.Aplication.Queries.TipoGastoQueries;

namespace PruebaTecnica1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoGastoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TipoGastoController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// POST /api/tipogasto
        /// Body: CreateTipoGastoCommand { Nombre, Descripcion }
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateTipoGastoCommand cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd.Nombre))
                return BadRequest("El nombre del tipo de gasto no puede estar vacio");
            try
            {
                var id = await _mediator.Send(cmd);
                return CreatedAtAction(nameof(GetById), new { id }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// GET /api/tipogasto
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _mediator.Send(new GetAllTipoGastoQuery());
            return Ok(list);
        }

        /// <summary>
        /// GET /api/tipogasto/{id}
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var dto = await _mediator.Send(new GetTipoGastoByIdQuery(id));
                return Ok(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"EL Tipo de gasto con id: {id} no ha sido encontrado");
            }
        }

        /// <summary>
        /// PUT /api/tipogasto/{id}
        /// Body: UpdateTipoGastoCommand { Id, Nombre, Descripcion }
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTipoGastoCommand cmd)
        {
            if (id != cmd.Id) return BadRequest("El Id en URL y en cuerpo no coinciden.");

            try
            {
                await _mediator.Send(cmd);
                return NoContent();
            }
            catch (DBConcurrencyException)
            {
                return Conflict($"El tipo de gasto con id {id} ya ha sido modificado por otro proceso");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"El tipo de gasto con id {id} no ha sido encontrado");
            }
        }

        /// <summary>
        /// DELETE /api/tipogasto/{id}
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteTipoGastoCommand(id));
                return NoContent();
            }
            catch (DBConcurrencyException)
            {
                return Conflict($"El tipo de gasto con id {id} ya ha sido eliminado por otro proceso");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"El tipo de gasto con id {id} no ha sido encontrado");
            }
        }
    }
}
