using MediatR;
using PruebaTecnica1.Application.DTOs;
using PruebaTecnica1.Application.Services;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Application.Queries.Usuario
{
    public class LoginUsuarioQueryHandler : IRequestHandler<LoginUsuarioQuery, LoginResultDto>
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IPasswordHasher _hasher;

        public LoginUsuarioQueryHandler(
            IUsuarioRepository usuarioRepo,
            IPasswordHasher hasher)
        {
            _usuarioRepo = usuarioRepo;
            _hasher      = hasher;
        }

        public async Task<LoginResultDto> Handle(LoginUsuarioQuery req, CancellationToken ct)
        {
            // 1. Obtener usuario por nombre de usuario
            var usuario = await _usuarioRepo.GetByUsernameAsync(req.Username, ct);

            // 2. Verificar que la contraseña (texto plano) coincide con el hash
            if (!_hasher.Verify(req.PlainPassword,  usuario.PasswordHash))
                throw new UnauthorizedAccessException("Credenciales inválidas");

            // 3. Retornar resultado (puedes agregar más datos como token, etc.)
            return new LoginResultDto(usuario.Id, usuario.EsAdmin);
        }
    }
}
