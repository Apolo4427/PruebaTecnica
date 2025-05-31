using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Interface.Persistence.Data.Configurations
{
    public class TipoGastoConfiguration : IEntityTypeConfiguration<TipoGasto>
    {
        public void Configure(EntityTypeBuilder<TipoGasto> builder)
        {
            // Nombre de la tabla
            builder.ToTable("TipoGastos");

            // Clave primaria (Guid generado en el dominio)
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                   .ValueGeneratedNever();

            // VO CodigoTipoGasto → mapeado a columna "Codigo"
            builder.OwnsOne(
                t => t.Codigo,
                cb =>
                {
                    cb.Property(c => c.Value)
                      .HasColumnName("Codigo")
                      .IsRequired()
                      .HasMaxLength(8); // Formato "TG-0001"
                }
            );

            // VO Nombre → mapeado a columna "Nombre"
            builder.OwnsOne(
                t => t.Nombre,
                nb =>
                {
                    nb.Property(n => n.Value)
                      .HasColumnName("Nombre")
                      .IsRequired()
                      .HasMaxLength(100);
                }
            );

            // Descripción (string simple, opcional)
            builder.Property(t => t.Descripcion)
                   .HasColumnName("Descripcion")
                   .HasMaxLength(250)
                   .IsRequired(false);

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
