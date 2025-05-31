using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Core.Models
{
    public class Presupuesto : Entity<Guid>
    {
        public Guid  UsuarioId   { get; private set; }
        public Guid  TipoGastoId { get; private set; }
        public int   Anio        { get; private set; }
        public int   Mes         { get; private set; }
        public Money Monto       { get; private set; }

        public byte[] RowVersion  { get; private set; }

        private Presupuesto() { }

        public static Presupuesto Create(
            Guid usuarioId,
            Guid tipoGastoId,
            int anio,
            int mes,
            Money monto)
        {
            if (usuarioId   == Guid.Empty) throw new ArgumentException("UsuarioId es obligatorio.", nameof(usuarioId));
            if (tipoGastoId == Guid.Empty) throw new ArgumentException("TipoGastoId es obligatorio.", nameof(tipoGastoId));
            if (anio < 1)                  throw new ArgumentException("El año debe ser positivo.", nameof(anio));
            if (mes  < 1 || mes > 12)      throw new ArgumentException("El mes debe estar entre 1 y 12.", nameof(mes));
            if (monto is null)             throw new ArgumentNullException(nameof(monto), "El monto es obligatorio.");

            return new Presupuesto
            {
                // id asignado por guid secuencial en SQL Server
                UsuarioId    = usuarioId,
                TipoGastoId  = tipoGastoId,
                Anio         = anio,
                Mes          = mes,
                Monto        = monto
            };
        }

        public void UpdateMonto(Money nuevoMonto)
        {
            if (nuevoMonto is null)
                throw new ArgumentNullException(nameof(nuevoMonto), "El nuevo monto es obligatorio.");
            Monto = nuevoMonto;
        }
    }
}
