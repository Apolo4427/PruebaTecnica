using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Interface.Persistence.Data.Configurations
{
    public class FondoMonetarioConfiguration : IEntityTypeConfiguration<FondoMonetario>
    {
        public void Configure(EntityTypeBuilder<FondoMonetario> builder)
        {
            builder.ToTable("FondosMonetarios");

            // Clave primaria (Guid generado en el dominio)
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                   .ValueGeneratedNever();

            // VO Nombre → mapeado a columna "Nombre"
            builder.OwnsOne(
                f => f.Nombre,
                nb =>
                {
                    nb.Property(n => n.Value)
                      .HasColumnName("Nombre")
                      .IsRequired()
                      .HasMaxLength(100);
                }
            );

            // Enum TipoFondo → guardado como texto (string)
            builder.Property(f => f.Tipo)
                   .HasColumnName("Tipo")
                   .HasConversion<string>()
                   .IsRequired();

            // VO Saldo (Money) → mapeado a columna "Saldo"
            builder.OwnsOne(
                f => f.Saldo,
                sb =>
                {
                    sb.Property(m => m.Amount)
                      .HasColumnName("Saldo")
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
