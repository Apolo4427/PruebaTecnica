using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Interface.Persistence.Data.Configurations
{
    public class TipoGastoConfiguration : IEntityTypeConfiguration<TipoGasto>
    {
        public void Configure(EntityTypeBuilder<TipoGasto> builder)
        {
            // Nombre de la tabla
            builder.ToTable("TipoGastos");

            // VO CodigoTipoGasto → mapeado a columna "Codigo"
            builder.Property(t => t.Codigo)
                .HasColumnName("Codigo")
                .HasMaxLength(8)
                .IsRequired()
                .HasConversion(
                    vo => vo.Value,
                    str => CodigoTipoGasto.Create(str)
                );

            // VO Nombre → mapeado a columna "Nombre"
            builder.Property(t => t.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(100)
                .IsRequired()
                .HasConversion(
                    vo => vo.Value,
                    str => Nombre.Create(str)
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
