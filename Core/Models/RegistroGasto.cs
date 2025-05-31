using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Core.Models
{
    public enum TipoDocumento
    {
        Comprobante,
        Factura,
        Otro
    }
    /// <summary>
    /// Encabezado de un registro de gasto, con su lista de detalles.
    /// </summary>
    public class RegistroGasto : Entity<Guid>
    {
        public Guid UsuarioId { get; private set; }
        public DateTime Fecha { get; private set; }
        public Guid FondoMonetarioId { get; private set; }
        public string NombreComercio { get; private set; }
        public TipoDocumento TipoDocumento { get; private set; }
        public string Observaciones { get; private set; }

        private readonly List<GastoDetalle> _detalles = new();
        public IReadOnlyCollection<GastoDetalle> Detalles => _detalles.AsReadOnly();

        public byte[] RowVersion { get; private set; }

        private RegistroGasto() { }

        public static RegistroGasto Create(
            Guid usuarioId,
            Guid fondoMonetarioId,
            DateTime fecha,
            string nombreComercio,
            TipoDocumento tipoDocumento,
            string observaciones = null)
        {
            if (usuarioId == Guid.Empty)
                throw new ArgumentException("UsuarioId es obligatorio.", nameof(usuarioId));
            if (fondoMonetarioId == Guid.Empty)
                throw new ArgumentException("FondoMonetarioId es obligatorio.", nameof(fondoMonetarioId));
            if (fecha == default)
                throw new ArgumentException("La fecha es obligatoria.", nameof(fecha));
            if (string.IsNullOrWhiteSpace(nombreComercio))
                throw new ArgumentException("El nombre de comercio es obligatorio.", nameof(nombreComercio));

            return new RegistroGasto
            {
                Id = Guid.NewGuid(),
                UsuarioId = usuarioId,
                FondoMonetarioId = fondoMonetarioId,
                Fecha = fecha,
                NombreComercio = nombreComercio.Trim(),
                TipoDocumento = tipoDocumento,
                Observaciones = observaciones
            };
        }

        /// <summary>
        /// Agrega un detalle al registro. Lanza si monto nulo o tipo vacío.
        /// </summary>
        public void AddDetalle(Guid tipoGastoId, Money monto)
        {
            _detalles.Add(new GastoDetalle(tipoGastoId, monto));
        }

        /// <summary>
        /// Asegura que haya al menos un detalle antes de guardar.
        /// </summary>
        public void EnsureDetails()
        {
            if (!_detalles.Any())
                throw new InvalidOperationException("Debe existir al menos un detalle de gasto.");
        }
    }
}