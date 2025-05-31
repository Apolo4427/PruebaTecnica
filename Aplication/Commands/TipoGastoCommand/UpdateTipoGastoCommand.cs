using MediatR;

namespace PruebaTecnica1.Aplication.Commands.TipoGastoCommand
{
    public record UpdateTipoGastoCommand(
        Guid Id,
        string Nombre,
        string Descripcion
    ) : IRequest;
}