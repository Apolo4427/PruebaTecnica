using MediatR;
using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Aplication.Commands.FondoMonetarioCommand
{
    public record UpdateFondoMonetarioCommand(
        Guid Id,
        string Nombre,
        TipoFondo Tipo
    ) : IRequest;
}