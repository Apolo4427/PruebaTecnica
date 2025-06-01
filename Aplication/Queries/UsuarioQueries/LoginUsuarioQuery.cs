using MediatR;
using PruebaTecnica1.Aplication.DTOs;

namespace PruebaTecnica1.Application.Queries.UsuarioQueries
{
    /// <summary>
    /// Query para autenticación: recibe username y contraseña en texto plano.
    /// </summary>
    public record LoginUsuarioQuery(string Username, string PlainPassword) : IRequest<LoginResultDto>;
}
