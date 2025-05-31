using MediatR;
using PruebaTecnica1.Aplication.Commands.RegistroGastoCommand;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Handlers.RegsitroGastoHandler
{
    public class CreateRegistroGastoCommandHandler : IRequestHandler<CreateRegistroGastoCommand, Guid>
    {
        private readonly IRegistroGastoRepository _registroRepo;
        private readonly IFondoMonetarioRepository _fondoRepo;
        private readonly IPresupuestoRepository _presRepo;

        public CreateRegistroGastoCommandHandler(
            IRegistroGastoRepository r,
            IFondoMonetarioRepository f,
            IPresupuestoRepository p)
        {
            _registroRepo = r;
            _fondoRepo = f;
            _presRepo = p;
        }

        public async Task<Guid> Handle(CreateRegistroGastoCommand req, CancellationToken ct)
        {
            // 1. Valida presupuesto y posibles sobres
            var presupuestos = await _presRepo.GetByUsuarioAndMesAsync(req.UsuarioId, req.Fecha.Year, req.Fecha.Month, ct)
                ?? throw new KeyNotFoundException(
                    $"El usuario con id: {req.UsuarioId} no ha sido encontrado"
                );
                
            //    b) Si no hay presupuesto para algún TipoGasto, asumimos monto 0
            var presDict = presupuestos.ToDictionary(p => p.TipoGastoId, p => p.Monto.Amount);

            //    c) Obtener todos los gastos previos del usuario en el mismo mes
            var mesInicio = new DateTime(req.Fecha.Year, req.Fecha.Month, 1);
            var mesFin = mesInicio.AddMonths(1).AddTicks(-1);
            var registrosEnMes = await _registroRepo.GetByRangoFechaAsync(mesInicio, mesFin, ct);
            var registrosUsuario = registrosEnMes.Where(r => r.UsuarioId == req.UsuarioId);

            //    d) Sumar montos previos por TipoGasto
            var gastadoPorTipoPrevio = registrosUsuario
                .SelectMany(r => r.Detalles)
                .GroupBy(d => d.TipoGastoId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(d => d.Monto.Amount)
                );

            //    e) Ahora revisamos cada detalle nuevo para ver si excede
            var sobregiros = new List<string>();
            foreach (var detalle in req.Detalles)
            {
                var tipoId = detalle.TipoGastoId;
                var montoNuevo = detalle.Monto;

                presDict.TryGetValue(tipoId, out var montoPresupuestado);
                gastadoPorTipoPrevio.TryGetValue(tipoId, out var montoPrevio);

                var totalConNuevo = montoPrevio + montoNuevo;
                if (totalConNuevo > montoPresupuestado)
                {
                    var exceso = totalConNuevo - montoPresupuestado;
                    sobregiros.Add($"TipoGasto {tipoId}: presupuestado {montoPresupuestado:F2}, exceso {exceso:F2}");
                }
            }

            if (sobregiros.Any())
            {
                var mensaje = "Sobregiro detectado en los siguientes ítems:\n" +
                              string.Join("\n", sobregiros);
                throw new InvalidOperationException(mensaje);
            }


            // 2. Ajuste de saldo en fondo
            foreach (var d in req.Detalles)
            {
                await _fondoRepo.AjustarSaldoAsync(req.FondoMonetarioId, d.Monto, false, ct);
            }

            // 3. Crear y guardar registro de gasto
            var entity = RegistroGasto.Create(
                req.UsuarioId, req.FondoMonetarioId, req.Fecha,
                req.NombreComercio, req.TipoDocumento, req.Observaciones
            );
            foreach (var d in req.Detalles)
                entity.AddDetalle(d.TipoGastoId, Money.FromDecimal(d.Monto));

            await _registroRepo.AddWithDetailsAsync(entity, ct);
            return entity.Id;
        }
    }
}