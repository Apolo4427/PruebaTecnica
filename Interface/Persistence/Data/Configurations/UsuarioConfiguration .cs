using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Interface.Persistence.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            // Clave primaria (Guid generado en el dominio)
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                   .ValueGeneratedNever();

            // VO Username → mapeado a columna "NombreUsuario"
            builder.OwnsOne(
                u => u.NombreUsuario,
                ub =>
                {
                    ub.Property(n => n.Value)
                      .HasColumnName("NombreUsuario")
                      .HasMaxLength(20)
                      .IsRequired();
                }
            );

            // VO PlainPassword (el valor que se almacena es el hash) → mapeado a "PasswordHash"
            builder.OwnsOne(
                u => u.PasswordHash,
                pb =>
                {
                    pb.Property(p => p.Value)
                      .HasColumnName("PasswordHash")
                      .HasMaxLength(200)
                      .IsRequired();
                }
            );

            builder.Property(u => u.EsAdmin)
                   .HasColumnName("EsAdmin")
                   .IsRequired();
                
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
