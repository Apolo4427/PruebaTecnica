using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Core.Ports.Repositories
{
    public interface IRegistroGastoRepository
    {
        /// <summary>
        /// Inserta encabezado + detalles en transacción y arroja excepción si algo falla.
        /// </summary>
        Task AddWithDetailsAsync(RegistroGasto encabezado,  CancellationToken cancellationToken = default);
        
        Task<RegistroGasto> GetByIdAsync(Guid id,  CancellationToken cancellationToken = default);
        Task<IReadOnlyList<RegistroGasto>> GetByRangoFechaAsync(DateTime desde, DateTime hasta,  CancellationToken cancellationToken = default);
    }
}
