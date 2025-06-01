using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica1.Aplication.DTOs;
using PruebaTecnica1.Aplication.Queries.UsuarioQueries;

namespace PruebaTecnica1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{UserName}")]
        public async Task<ActionResult<UserDto>> GetUsuarioByUserName(string UserName)
        {
            try
            {
                var query = new GetUsuarioByUsernameQuery(UserName);
                var dto = await _mediator.Send(query);
                return Ok(dto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"El usuario con nombre {UserName} no ha sido encontrado");
            }
        }
    }
}
