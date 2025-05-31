using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Interface.Persistence.Data.Configurations
{
    public class RegistroGastoConfiguration : IEntityTypeConfiguration<RegistroGasto>
    {
        public void Configure(EntityTypeBuilder<RegistroGasto> builder)
        {
            builder.ToTable("RegistrosGasto");

            // Clave primaria (Guid generado en el dominio)
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                   .ValueGeneratedNever();

            // Propiedades primitivas
            builder.Property(r => r.UsuarioId)
                   .HasColumnName("UsuarioId")
                   .IsRequired();

            builder.Property(r => r.Fecha)
                   .HasColumnName("Fecha")
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.Property(r => r.FondoMonetarioId)
                   .HasColumnName("FondoMonetarioId")
                   .IsRequired();

            builder.Property(r => r.NombreComercio)
                   .HasColumnName("NombreComercio")
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(r => r.TipoDocumento)
                   .HasColumnName("TipoDocumento")
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(r => r.Observaciones)
                   .HasColumnName("Observaciones")
                   .HasMaxLength(500)
                   .IsRequired(false);

            // Owned Collection: GastoDetalle
            builder.OwnsMany(
                r => r.Detalles,
                db =>
                {
                    db.ToTable("GastoDetalles");

                    // Clave primaria (campo sombra "Id")
                    db.HasKey("Id");
                    db.Property<Guid>("Id")
                      .HasColumnName("Id");

                    // FK hacia RegistroGasto
                    db.WithOwner()
                      .HasForeignKey("RegistroGastoId");

                    // TipoGastoId dentro de GastoDetalle
                    db.Property(d => d.TipoGastoId)
                      .HasColumnName("TipoGastoId")
                      .IsRequired();

                    // VO Monto dentro de GastoDetalle → mapeado a "Monto"
                    db.OwnsOne(
                        d => d.Monto,
                        mb =>
                        {
                            mb.Property(m => m.Amount)
                              .HasColumnName("Monto")
                              .HasColumnType("decimal(18,2)")
                              .IsRequired();
                        }
                    );
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
