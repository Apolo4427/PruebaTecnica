using MediatR;
using PruebaTecnica1.Aplication.DTOs;

namespace PruebaTecnica1.Aplication.Queries.UsuarioQueries
{
    public record GetUsuarioByUsernameQuery(
        string Username
    ):IRequest<UserDto>;
}