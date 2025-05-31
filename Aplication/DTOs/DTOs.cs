using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Aplication.DTOs
{
    public record TipoGastoDto(Guid Id, string Codigo, string Nombre, string Descripcion);

    public record FondoMonetarioDto(Guid Id, string Nombre, TipoFondo Tipo, decimal Saldo);

    public record PresupuestoDto(Guid Id, Guid TipoGastoId, int Anio, int Mes, decimal Monto);

    public enum MovimientoTipo { Gasto, Deposito }
    public record MovimientoDto
    {
        public Guid Id { get; init; }
        public DateTime Fecha { get; init; }
        public MovimientoTipo Tipo { get; init; }
        public Guid? TipoGastoId { get; init; }
        public decimal Monto { get; init; }
        public string NombreComercio { get; init; }
        public string TipoDocumento { get; init; }
        public Guid FondoMonetarioId { get; init; }
    }

    public record ComparativoDto
    {
        public Guid TipoGastoId { get; init; }
        public int Anio { get; init; }
        public int Mes { get; init; }
        public decimal MontoPresupuestado { get; init; }
        public decimal MontoEjecutado { get; init; }
    }

}