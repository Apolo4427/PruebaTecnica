using MediatR;

namespace PruebaTecnica1.Aplication.Commands.TipoGastoCommand
{
    public record DeleteTipoGastoCommand(
        Guid Id
    ) : IRequest;

}