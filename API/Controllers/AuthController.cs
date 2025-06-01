using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica1.Application.Queries.UsuarioQueries;

namespace PruebaTecnica1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// POST /api/auth/login
        /// Recibe LoginUsuarioQuery { Username, PlainPassword } y retorna LoginResultDto.
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return Unauthorized("Usuario no encontrado");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Contraseña incorrecta");
            }
        }
    }
}
