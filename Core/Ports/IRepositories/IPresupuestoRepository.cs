using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Core.Ports.Repositories
{
    public interface IPresupuestoRepository
    {
        Task AddAsync(Presupuesto entity,  CancellationToken cancellationToken = default);
        Task<Presupuesto> GetByUsuarioTipoYMesAsync(
            Guid usuarioId, Guid tipoGastoId, int año, int mes,  CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Presupuesto>> GetByUsuarioAndMesAsync(
            Guid usuarioId, int año, int mes,  CancellationToken cancellationToken = default);
    }
}
