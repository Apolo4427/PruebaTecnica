using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Core.Models
{
    public enum TipoFondo
    {
        CuentaBancaria,
        CajaMenuda
    }

    public class FondoMonetario : Entity<Guid>
    {
        public Nombre Nombre { get; private set; }
        public TipoFondo Tipo { get; private set; }

        /// <summary>
        /// Saldo actual del fondo; se ajusta con movimientos y depósitos.
        /// </summary>
        public Money Saldo { get; private set; }

        // Token de concurrencia
        public byte[] RowVersion { get; private set; }

        private FondoMonetario() { }

        public static FondoMonetario Create(Nombre nombre, TipoFondo tipo, Money saldoInicial)
        {
            if (nombre is null)
                throw new ArgumentException("El nombre del fondo es obligatorio.", nameof(nombre));

            return new FondoMonetario
            {
                // id asignado por guid secuencial en SQL Server
                Nombre = nombre,
                Tipo = tipo,
                Saldo = saldoInicial ?? Money.FromDecimal(0m)
            };
        }

        /// Ajusta el saldo: si es depósito, suma; si es gasto, resta.
        /// </summary>
        /// <param name="monto">Monto del ajuste (siempre positivo).</param>
        /// <param name="esDeposito">True para depósitos; false para gastos.</param>
        public void AjustarSaldo(Money monto, bool esDeposito)
        {
            if (monto is null)
                throw new ArgumentNullException(nameof(monto));

            Saldo = esDeposito
                ? Saldo.Add(monto)
                : Saldo.Subtract(monto);
        }

        public void CambiarNombre(Nombre nuevoNombre)
        {
            if (nuevoNombre is null)
                throw new ArgumentException("El nombre no puede estar vacio");

            if (nuevoNombre.Equals(Nombre))
                return;

            Nombre = nuevoNombre;
        }

        public void CambiarTipo(TipoFondo nuevoTipo)
        {
            if (nuevoTipo.Equals(Tipo))
                return;
            Tipo = nuevoTipo;
        }

        public void CambiarSaldo(Money nuevoSaldo)
        {
            if (nuevoSaldo is null)
                throw new ArgumentException("El saldo no puede ser nulo");
            if (nuevoSaldo.Equals(Saldo))
                return;
            Saldo = nuevoSaldo;
        }
    }
}
