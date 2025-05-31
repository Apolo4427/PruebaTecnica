using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Core.Models
{
    public class Deposito : Entity<Guid>
    {
        public Guid     UsuarioId        { get; private set; }
        public Guid     FondoMonetarioId { get; private set; }
        public DateTime Fecha            { get; private set; }
        public Money    Monto            { get; private set; }

        public byte[]   RowVersion       { get; private set; }

        private Deposito() { }

        public static Deposito Create(
            Guid usuarioId,
            Guid fondoMonetarioId,
            DateTime fecha,
            Money monto)
        {
            if (usuarioId == Guid.Empty)
                throw new ArgumentException("UsuarioId es obligatorio.", nameof(usuarioId));
            if (fondoMonetarioId == Guid.Empty)
                throw new ArgumentException("FondoMonetarioId es obligatorio.", nameof(fondoMonetarioId));
            if (fecha == default)
                throw new ArgumentException("La fecha es obligatoria.", nameof(fecha));
            if (monto is null)
                throw new ArgumentNullException(nameof(monto), "El monto es obligatorio.");

            return new Deposito
            {
                //Hacer el id asignado por guid secuencial en el concext para evitar fragmentacion
                UsuarioId        = usuarioId,
                FondoMonetarioId = fondoMonetarioId,
                Fecha            = fecha,
                Monto            = monto
            };
        }
    }
}
