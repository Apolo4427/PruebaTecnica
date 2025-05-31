using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Core.Models
{
    public class Usuario : Entity<Guid>
    {
        public Username NombreUsuario { get; private set; }
        public PlainPassword PasswordHash { get; private set; }
        public bool EsAdmin { get; private set; }

        public byte[] RowVersion { get; private set; }

        private Usuario() { }

        /// <summary>
        /// Crea un nuevo usuario. El passwordHash debe llegar ya hasheado.
        /// </summary>
        public static Usuario Create(Username nombreUsuario, PlainPassword passwordHash, bool esAdmin = false)
        {
            if (nombreUsuario is null)
                throw new ArgumentException("El nombre de usuario es obligatorio.", nameof(nombreUsuario));
            if (passwordHash is null)
                throw new ArgumentException("El hash de la contraseña es obligatorio.", nameof(passwordHash));

            return new Usuario
            {
                // id asignado por guid secuencial en SQL Server
                NombreUsuario = nombreUsuario,
                PasswordHash = passwordHash,
                EsAdmin = esAdmin
            };
        }

        /// <summary>
        /// Cambia el hash de contraseña por uno nuevo (previamente generado).
        /// </summary>
        public void CambiarPassword(PlainPassword nuevoPasswordHash)
        {
            if (nuevoPasswordHash is null)
                throw new ArgumentException(
                    "El hash de la nueva contraseña es obligatorio.",
                    nameof(nuevoPasswordHash)
                );

            PasswordHash = nuevoPasswordHash;
        }

        public void CambiarNombre(Username nuevoNombre)
        {
            if (nuevoNombre is null)
                throw new ArgumentException("El nombre no puede estar vacio");
            if (nuevoNombre.Equals(NombreUsuario))
                return;
        
            NombreUsuario = nuevoNombre;
        }

        public void CambiarAdmin(bool esAdmin)
        {
            EsAdmin = esAdmin;
        }
    }
}
