using MediatR;

namespace PruebaTecnica1.Aplication.Commands.DepositoCommnad
{
    public record CreateDepositoCommand(
        Guid UsuarioId,
        Guid FondoMonetarioId,
        DateTime Fecha,
        decimal Monto
    ) : IRequest<Guid>;
}