using PruebaTecnica1.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaTecnica1.Core.Ports.Repositories
{
    public interface IFondoMonetarioRepository
    {
        Task AddAsync(FondoMonetario entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(FondoMonetario entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<FondoMonetario> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<FondoMonetario>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Actualizar el valor por posibles errores
        /// </summary>
        Task UpdateSaldoAsync(Guid fondoId, decimal nuevoSaldo, CancellationToken cancellationToken = default);

        /// <summary>
        /// Ajusta el saldo del fondo (depósitos/gastos).
        /// </summary>
        Task AjustarSaldoAsync(Guid fondoId, decimal ajusteSaldo, bool esDeposito, CancellationToken cancellationToken = default);
    }
}