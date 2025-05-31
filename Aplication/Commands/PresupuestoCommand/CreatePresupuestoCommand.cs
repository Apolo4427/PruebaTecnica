using MediatR;

public record CreatePresupuestoCommand(
    Guid UsuarioId,
    Guid TipoGastoId,
    int Anio,
    int Mes,
    decimal Monto
) : IRequest<Guid>;