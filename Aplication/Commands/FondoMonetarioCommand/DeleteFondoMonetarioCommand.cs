using MediatR;

namespace PruebaTecnica1.Aplication.Commands.FondoMonetarioCommand
{
    public record DeleteFondoMonetarioCommand(
        Guid Id
    ) : IRequest;
}