using MediatR;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Core.Ports.Repositories;

public class CreatePresupuestoCommandHandler : IRequestHandler<CreatePresupuestoCommand, Guid>
{
    private readonly IPresupuestoRepository _repo;
    public CreatePresupuestoCommandHandler(IPresupuestoRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreatePresupuestoCommand req, CancellationToken ct)
    {
        var montoVo = Money.FromDecimal(req.Monto);
        var entity  = Presupuesto.Create(req.UsuarioId, req.TipoGastoId, req.Anio, req.Mes, montoVo);
        await _repo.AddAsync(entity, ct);
        return entity.Id;
    }
}