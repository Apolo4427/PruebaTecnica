using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Interface.Persistence.Data.Configurations
{
    public class DepositoConfiguration : IEntityTypeConfiguration<Deposito>
    {
        public void Configure(EntityTypeBuilder<Deposito> builder)
        {
            builder.ToTable("Depositos");

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
            builder.Property(d => d.Monto)
                   .HasColumnName("Monto")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired()
                   .HasConversion(
                       vo => vo.Amount,               // De Money → decimal
                       amt => Money.Create(amt)       // De decimal → Money
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
