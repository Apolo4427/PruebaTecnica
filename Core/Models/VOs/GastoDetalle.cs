namespace PruebaTecnica1.Core.Models.VOs
{
    /// <summary>
    /// Detalle contenido dentro de un registro de gasto.
    /// </summary>
    public sealed class GastoDetalle : ValueObject
    {
        public Guid  TipoGastoId { get; }
        public Money Monto       { get; }

        public GastoDetalle(Guid tipoGastoId, Money monto)
        {
            if (tipoGastoId == Guid.Empty)
                throw new ArgumentException("El TipoGastoId es obligatorio.", nameof(tipoGastoId));
            if (monto is null)
                throw new ArgumentNullException(nameof(monto), "El monto es obligatorio.");

            TipoGastoId = tipoGastoId;
            Monto       = monto;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TipoGastoId;
            yield return Monto;
        }
    }
}