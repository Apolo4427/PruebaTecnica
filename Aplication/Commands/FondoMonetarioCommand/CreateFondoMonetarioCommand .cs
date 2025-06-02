using MediatR;
using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Aplication.Commands.FondoMonetarioCommand
{
    public record CreateFondoMonetarioCommand(
        string Nombre,
        TipoFondo Tipo,
        decimal SaldoInicial
    ) : IRequest<Guid>;
}