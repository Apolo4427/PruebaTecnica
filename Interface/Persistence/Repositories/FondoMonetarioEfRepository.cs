using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Core.Ports.Repositories;
using PruebaTecnica1.Interface.Persistence.Data;

namespace PruebaTecnica1.Interface.Persistence.Repositories
{
    public class FondoMonetarioEfRepository : IFondoMonetarioRepository
    {
        private readonly AppDbContext _db;
        public FondoMonetarioEfRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(FondoMonetario entity,  CancellationToken cancellationToken = default)
        {
            _db.FondosMonetarios.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(FondoMonetario entity,  CancellationToken cancellationToken = default)
        {

            try
            {
                var entry = _db.Entry(entity);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException(
                    $"No se pudo actualizar el fondo monetario con ID {entity.Id} porque fue modificado por otro proceso."
                );

            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.FondosMonetarios.FindAsync(id);
            if (entity != null)
            {
                _db.FondosMonetarios.Remove(entity);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<FondoMonetario> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _db.FondosMonetarios.FindAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException(
                    $"El fondo monetario de id: {id} no ha sido encontrado"
                );
        public async Task<IReadOnlyList<FondoMonetario>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _db.FondosMonetarios.AsNoTracking().ToListAsync(cancellationToken); // se podria paginar para grandes cantidades de datos

        public async Task UpdateSaldoAsync(Guid fondoId, decimal nuevoSaldo, CancellationToken cancellationToken = default)
        {
            var fondo = await _db.FondosMonetarios.FindAsync(fondoId)
                ?? throw new KeyNotFoundException(
                    $"El fondo monetario de id {fondoId} no ha sido encontrado"
                );
            fondo.CambiarSaldo(Money.FromDecimal(nuevoSaldo));
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task AjustarSaldoAsync(Guid fondoId, decimal ajusteSaldo, bool esDeposito, CancellationToken cancellationToken = default)
        {
            var fondo = await _db.FondosMonetarios.FindAsync(fondoId)// la entidad viene con trakig automatico
                ?? throw new KeyNotFoundException(
                    $"El fondo monetario de id {fondoId} no ha sido encontrado"
                );
            var money = Money.FromDecimal(ajusteSaldo);

            fondo.AjustarSaldo(money, esDeposito);

            try
            {
                var entry = _db.Entry(fondo);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException(
                    $"Conflicto de concurrencia al intentar actualizar TipoGasto con ID {fondo.Id}."
                );
            }

        }
    }
}