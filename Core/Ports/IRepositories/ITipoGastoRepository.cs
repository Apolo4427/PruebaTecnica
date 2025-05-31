using PruebaTecnica1.Core.Models;
namespace PruebaTecnica1.Core.Ports.Repositories
{
    public interface ITipoGastoRepository
    {
        /// <summary>
        /// Genera el siguiente código (p.ej. "TG-0002") de forma automática.
        /// </summary>
        Task<string> GetNextCodigoAsync( CancellationToken cancellationToken = default);

        Task AddAsync(TipoGasto entity,  CancellationToken cancellationToken = default);
        Task UpdateAsync(TipoGasto entity,  CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id,  CancellationToken cancellationToken = default);
        Task<TipoGasto> GetByIdAsync(Guid id,  CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TipoGasto>> GetAllAsync( CancellationToken cancellationToken = default);
    }
}
