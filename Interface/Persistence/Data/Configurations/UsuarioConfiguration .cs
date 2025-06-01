using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Interface.Persistence.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            // VO Username → mapeado a columna "NombreUsuario"
            builder.Property(u => u.NombreUsuario)
                   .HasColumnName("NombreUsuario")
                   .HasMaxLength(20)
                   .IsRequired()
                   .HasConversion(
                       vo => vo.Value,                  // De UsernameVO → string
                       str => Username.Create(str)      // De string → UsernameVO
                   );

            // VO PlainPassword (el valor que se almacena es el hash) → mapeado a "PasswordHash"
            builder.Property(u => u.PasswordHash)
                   .HasColumnName("PasswordHash")
                   .HasMaxLength(200)
                   .IsRequired()
                   .HasConversion(
                       vo => vo.Value,                       // De PlainPasswordVO → string
                       str => PlainPassword.Create(str)      // De string → PlainPasswordVO
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
