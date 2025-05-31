using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Ports.Repositories;
using PruebaTecnica1.Interface.Persistence.Data;

namespace PruebaTecnica1.Interface.Persistence.Repositories
{
    public class DepositoEfRepository : IDepositoRepository
    {
        private readonly AppDbContext _db;
        public DepositoEfRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(Deposito entity, CancellationToken cancellationToken = default)
        {
            _db.Depositos.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Deposito>> GetByRangoFechaAsync(DateTime desde, DateTime hasta, CancellationToken cancellationToken = default) =>
            await _db.Depositos
                .AsNoTracking()
               .Where(d => d.Fecha >= desde && d.Fecha <= hasta)
               .ToListAsync(cancellationToken);
    }
}