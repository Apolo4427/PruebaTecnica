using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Core.Models
{
    public class TipoGasto : Entity<Guid>
    {
        /// <summary>
        /// Código generado automáticamente (p.ej. “TG-0001”)
        /// </summary>
        public CodigoTipoGasto Codigo { get; private set; }

        public Nombre Nombre { get; private set; }
        public string Descripcion { get; private set; }

        // Token de concurrencia para EF Core
        public byte[] RowVersion { get; private set; }

        
        private TipoGasto() { }

        // Factory method para crear un nuevo TipoGasto
        public static TipoGasto Create(CodigoTipoGasto codigo, Nombre nombre, string descripcion = null)
        {
            if (codigo is null)
                throw new ArgumentException("El código no puede estar vacío.", nameof(codigo));
            if (nombre is null)
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

            return new TipoGasto
            {
                Id = Guid.NewGuid(),
                Codigo = codigo,
                Nombre = nombre,
                Descripcion = descripcion
            };
        }

        // Método para actualizar datos
        public void Update(Nombre nombre, string descripcion)
        {
            if (nombre is null)
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

            Nombre = nombre;
            Descripcion = descripcion;
        }
    }
}
