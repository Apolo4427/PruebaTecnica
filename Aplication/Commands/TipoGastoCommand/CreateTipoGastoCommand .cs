using MediatR;

namespace PruebaTecnica1.Aplication.Commands.TipoGastoCommand
{
    public record CreateTipoGastoCommand(
        string Nombre,
        string Descripcion
    ) : IRequest<Guid>;
}