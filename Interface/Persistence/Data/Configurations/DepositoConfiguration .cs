using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Interface.Persistence.Data.Configurations
{
    public class DepositoConfiguration : IEntityTypeConfiguration<Deposito>
    {
        public void Configure(EntityTypeBuilder<Deposito> builder)
        {
            builder.ToTable("Depositos");

            // Clave primaria (Guid generado en el dominio)
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id)
                   .ValueGeneratedNever();

            // Propiedades primitivas
            builder.Property(d => d.UsuarioId)
                   .HasColumnName("UsuarioId")
                   .IsRequired();

            builder.Property(d => d.FondoMonetarioId)
                   .HasColumnName("FondoMonetarioId")
                   .IsRequired();

            builder.Property(d => d.Fecha)
                   .HasColumnName("Fecha")
                   .HasColumnType("datetime2")
                   .IsRequired();

            // VO Monto (Money) → mapeado a columna "Monto"
            builder.OwnsOne(
                d => d.Monto,
                mb =>
                {
                    mb.Property(m => m.Amount)
                      .HasColumnName("Monto")
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
                }
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
