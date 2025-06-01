// Interface/Persistence/Data/Configurations/PresupuestoConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Interface.Persistence.Data.Configurations
{
    public class PresupuestoConfiguration : IEntityTypeConfiguration<Presupuesto>
    {
        public void Configure(EntityTypeBuilder<Presupuesto> builder)
        {
            builder.ToTable("Presupuestos");

            // Propiedades primitivas
            builder.Property(p => p.UsuarioId)
                   .HasColumnName("UsuarioId")
                   .IsRequired();

            builder.Property(p => p.TipoGastoId)
                   .HasColumnName("TipoGastoId")
                   .IsRequired();

            builder.Property(p => p.Anio)
                   .HasColumnName("Anio")
                   .IsRequired();

            builder.Property(p => p.Mes)
                   .HasColumnName("Mes")
                   .IsRequired();

            // VO Monto (Money) → mapeado a columna "Monto"
            builder.Property(p => p.Monto)
                   .HasColumnName("Monto")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired()
                   .HasConversion(
                       vo => vo.Amount,           // De Money → decimal
                       amt => Money.Create(amt)   // De decimal → Money
                   );

            // Guid secuencial
            builder.Property(d => d.Id)
                   .HasColumnType("uniqueidentifier")
                   .HasDefaultValueSql("NEWSEQUENTIALID()")
                   .ValueGeneratedOnAdd();

            // RowVersion como token de concurrencia
            builder.Property<byte[]>("RowVersion")
                   .IsRowVersion()
                   .IsRequired();
        }
    }
}
