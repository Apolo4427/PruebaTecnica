using MediatR;
using PruebaTecnica1.Aplication.DTOs;
using PruebaTecnica1.Aplication.Queries.UsuarioQueries;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Handlers.UsuarioHandler
{
    public class GetUsuarioByUsernameHandler : IRequestHandler<GetUsuarioByUsernameQuery, UserDto>
    {
        private readonly IUsuarioRepository _repo;

        public GetUsuarioByUsernameHandler(IUsuarioRepository usuarioRepository)
        {
            _repo = usuarioRepository;
        }

        public async Task<UserDto> Handle(GetUsuarioByUsernameQuery request, CancellationToken cancellationToken)
        {
            var Usuario = await _repo.GetByUsernameAsync(request.Username, cancellationToken)
                ?? throw new KeyNotFoundException(
                    $"El usuario con nombre {request.Username} no ha sido encontrado"
                );

            
            return new UserDto(Usuario.NombreUsuario.ToString());
        }
    }

    public class UserName
    {
    }
}