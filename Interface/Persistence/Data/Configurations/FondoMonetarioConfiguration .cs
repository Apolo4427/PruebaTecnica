using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Interface.Persistence.Data.Configurations
{
    public class FondoMonetarioConfiguration : IEntityTypeConfiguration<FondoMonetario>
    {
        public void Configure(EntityTypeBuilder<FondoMonetario> builder)
        {
            builder.ToTable("FondosMonetarios");

            // VO Nombre → mapeado a columna "Nombre"
            builder.Property(f => f.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasConversion(
                       vo => vo.Value,               // De NombreVO → string
                       str => Nombre.Create(str)     // De string → NombreVO
                   );

            // Enum TipoFondo → guardado como texto (string)
            builder.Property(f => f.Tipo)
                   .HasColumnName("Tipo")
                   .HasConversion<string>()
                   .IsRequired();

            // VO Saldo (Money) → mapeado a columna "Saldo"
            builder.Property(f => f.Saldo)
                   .HasColumnName("Saldo")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired()
                   .HasConversion(
                       vo => vo.Amount,          // De Money → decimal
                       amt => Money.Create(amt)  // De decimal → Money
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
