using MediatR;
using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Aplication.Commands.RegistroGastoCommand
{
    public record CreateRegistroGastoCommand(
        Guid UsuarioId,
        Guid FondoMonetarioId,
        DateTime Fecha,
        string NombreComercio,
        TipoDocumento TipoDocumento,
        string Observaciones,
        List<(Guid TipoGastoId, decimal Monto)> Detalles
    ) : IRequest<Guid>;
}