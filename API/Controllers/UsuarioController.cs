using MediatR;
using Microsoft.AspNetCore.Mvc;
// (podríamos agregar Commands para registrar otros usuarios)

namespace PruebaTecnica1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator) => _mediator = mediator;

        // Por el momento solo login en AuthController, 
        // pero aquí podríamos exponer GET de perfil, etc.
    }
}
