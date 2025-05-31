using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Ports.Repositories;
using PruebaTecnica1.Interface.Persistence.Data;

namespace PruebaTecnica1.Interface.Persistence.Repositories
{
    public class RegistroGastoEfRepository : IRegistroGastoRepository
    {
        private readonly AppDbContext _db;
        public RegistroGastoEfRepository(AppDbContext db) => _db = db;

        public async Task AddWithDetailsAsync(RegistroGasto encabezado, CancellationToken cancellationToken =default)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken); // inicia una transacción en la base de datos de forma asíncrona.
            try
            {
                encabezado.EnsureDetails();
                _db.RegistrosGasto.Add(encabezado);
                await _db.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken); // materializamos la transaccion en la base de datos 
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<RegistroGasto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var registro = await _db.RegistrosGasto
                                .Include(r => r.Detalles)
                                .FirstOrDefaultAsync(r => r.Id == id);


            if (registro is null)
                throw new KeyNotFoundException(
                    $"El registro con id {id} no ha sido encontrado"
                );

            return registro;
        }

        public async Task<IReadOnlyList<RegistroGasto>> GetByRangoFechaAsync(DateTime desde, DateTime hasta, CancellationToken cancellationToken = default) =>
            await _db.RegistrosGasto
                .AsNoTracking()
                .Include(r => r.Detalles)
                .Where(r => r.Fecha >= desde && r.Fecha <= hasta)
                .ToListAsync(cancellationToken);
    }
}