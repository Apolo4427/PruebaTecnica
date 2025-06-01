using MediatR;
using PruebaTecnica1.Application.DTOs;

namespace PruebaTecnica1.Application.Queries.Usuario
{
    /// <summary>
    /// Query para autenticación: recibe username y contraseña en texto plano.
    /// </summary>
    public record LoginUsuarioQuery(string Username, string PlainPassword) : IRequest<LoginResultDto>;
}
