using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Ports.Repositories;
using PruebaTecnica1.Interface.Persistence.Data;

namespace PruebaTecnica1.Interface.Persistence.Repositories
{
    public class PresupuestoEfRepository : IPresupuestoRepository
    {
        private readonly AppDbContext _db;
        public PresupuestoEfRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(Presupuesto entity, CancellationToken cancellationToken = default)
        {
            _db.Presupuestos.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<Presupuesto> GetByUsuarioTipoYMesAsync(Guid usuarioId, Guid tipoGastoId, int año, int mes, CancellationToken cancellationToken = default) =>
            await _db.Presupuestos
               .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId
                                       && p.TipoGastoId == tipoGastoId
                                       && p.Anio == año
                                       && p.Mes == mes, cancellationToken)
                                        ?? throw new KeyNotFoundException(
                                            $"El presupuesto de id {usuarioId} no ha sido encontrado"
                                        );

        public async Task<IReadOnlyList<Presupuesto>> GetByUsuarioAndMesAsync(Guid usuarioId, int año, int mes, CancellationToken cancellationToken = default) =>
           await _db.Presupuestos
                .AsNoTracking()
                .Where(p => p.UsuarioId == usuarioId && p.Anio == año && p.Mes == mes)
                .ToListAsync(cancellationToken);
    }
}