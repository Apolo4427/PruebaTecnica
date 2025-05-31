using System.Data;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Ports.Repositories;
using PruebaTecnica1.Interface.Persistence.Data;

namespace PruebaTecnica1.Interface.Persistence.Repositories
{
    public class TipoGastoEfRepository : ITipoGastoRepository
    {
        private readonly AppDbContext _db;
        private const string Prefix = "TG-";

        public TipoGastoEfRepository(AppDbContext db) => _db = db;

        public async Task<string> GetNextCodigoAsync(CancellationToken cancellationToken = default)
        {
            var last = await _db.TipoGastos
                .OrderByDescending(t => t.Codigo)
                .Select(t => t.Codigo.ToString())
                .FirstOrDefaultAsync(cancellationToken);
            int next = 1;
            if (!string.IsNullOrEmpty(last) && last.StartsWith(Prefix) &&
                int.TryParse(last.Substring(Prefix.Length), out var num))
            {
                next = num + 1;
            }
            return $"{Prefix}{next:D4}";
        }

        public async Task AddAsync(TipoGasto entity, CancellationToken cancellationToken = default)
        {
            _db.TipoGastos.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TipoGasto entity, CancellationToken cancellationToken = default)
        {
            try
            {
                var entry = _db.Entry(entity);
                entry.State = EntityState.Modified;
                await _db.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("Conflicto de concurrencia al intentar ajustar el saldo del fondo.");
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.TipoGastos.FindAsync(
                    id ,
                    cancellationToken
            )
                ?? throw new KeyNotFoundException(
                    $"El tiepo de gasto con id {id} no ha sido encontrado"
                );
            _db.TipoGastos.Remove(entity);
            try
            {

                await _db.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException(
                    $"El tipo de gasco con el id {id} ya ha sido eliminado en otro proceso"
                );
            }
        }

        public async Task<TipoGasto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _db.TipoGastos.FindAsync(id, cancellationToken).AsTask()
                ?? throw new KeyNotFoundException(
                $"El tiepo de gasto con id {id} no ha sido encontrado"
            );

        public async Task<IReadOnlyList<TipoGasto>> GetAllAsync(
            CancellationToken cancellationToken = default
        ) => await _db.TipoGastos.AsNoTracking().ToListAsync(cancellationToken);
    }
}