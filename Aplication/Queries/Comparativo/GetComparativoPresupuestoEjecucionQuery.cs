using MediatR;
using PruebaTecnica1.Aplication.DTOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Queries.Comparativo
{
    public class GetComparativoPresupuestoEjecucionQuery : IRequest<List<ComparativoDto>>
    {
        public Guid UsuarioId { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }

    public class GetComparativoPresupuestoEjecucionQueryHandler : IRequestHandler<GetComparativoPresupuestoEjecucionQuery, List<ComparativoDto>>
    {
        private readonly IPresupuestoRepository _presRepo;
        private readonly IRegistroGastoRepository _gastoRepo;

        public GetComparativoPresupuestoEjecucionQueryHandler(
            IPresupuestoRepository presRepo,
            IRegistroGastoRepository gastoRepo)
        {
            _presRepo = presRepo;
            _gastoRepo = gastoRepo;
        }

        public async Task<List<ComparativoDto>> Handle(GetComparativoPresupuestoEjecucionQuery req, CancellationToken ct)
        {
            // 1. Obtener lista de registros de gasto en rango
            var gastos = await _gastoRepo.GetByRangoFechaAsync(req.Desde, req.Hasta, ct);
            var gastosUsuario = gastos.Where(r => r.UsuarioId == req.UsuarioId);

            // Agrupar ejecutado por TipoGasto
            var ejecutadoPorTipo = gastosUsuario
                .SelectMany(r => r.Detalles)
                .GroupBy(d => d.TipoGastoId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(d => d.Monto.Amount)
                );

            // 2. Obtener presupuestos mes a mes dentro del rango
            var comparativos = new List<ComparativoDto>();

            var current = new DateTime(req.Desde.Year, req.Desde.Month, 1);
            var end = new DateTime(req.Hasta.Year, req.Hasta.Month, 1);

            while (current <= end)
            {
                var presupuestos = await _presRepo.GetByUsuarioAndMesAsync(req.UsuarioId, current.Year, current.Month, ct);

                foreach (var p in presupuestos)
                {
                    // Monto ejecutado para ese tipoGasto en ese mes
                    gastos = gastosUsuario
                        .Where(r => r.Fecha.Year == current.Year && r.Fecha.Month == current.Month)
                        .ToList();

                    var totalEjecutado = gastos
                        .SelectMany(r => r.Detalles)
                        .Where(d => d.TipoGastoId == p.TipoGastoId)
                        .Sum(d => d.Monto.Amount);

                    comparativos.Add(new ComparativoDto
                    {
                        TipoGastoId = p.TipoGastoId,
                        Anio = current.Year,
                        Mes = current.Month,
                        MontoPresupuestado = p.Monto.Amount,
                        MontoEjecutado = totalEjecutado
                    });
                }

                current = current.AddMonths(1);
            }

            return comparativos;
        }
    }

}