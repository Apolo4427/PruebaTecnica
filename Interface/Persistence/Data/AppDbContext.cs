using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Interface.Persistence.Data.Configurations;

namespace PruebaTecnica1.Interface.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TipoGasto> TipoGastos { get; set; }
        public DbSet<FondoMonetario> FondosMonetarios { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<RegistroGasto> RegistrosGasto { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TipoGastoConfiguration());
            modelBuilder.ApplyConfiguration(new FondoMonetarioConfiguration());
            modelBuilder.ApplyConfiguration(new PresupuestoConfiguration());
            modelBuilder.ApplyConfiguration(new RegistroGastoConfiguration());
            modelBuilder.ApplyConfiguration(new DepositoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        }
    }
}