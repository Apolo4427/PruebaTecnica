using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Core.Ports.Repositories
{
    public interface IDepositoRepository
    {
        Task AddAsync(Deposito entity, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Deposito>> GetByRangoFechaAsync(DateTime desde, DateTime hasta, CancellationToken cancellationToken = default);
    }
}
