
using MediatR;
using PruebaTecnica1.Aplication.DTOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Queries.MovimientosQueries
{

    public class GetMovimientosPorRangoQuery : IRequest<List<MovimientoDto>>
    {
        public Guid UsuarioId { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }

    public class GetMovimientosPorRangoQueryHandler : IRequestHandler<GetMovimientosPorRangoQuery, List<MovimientoDto>>
    {
        private readonly IRegistroGastoRepository _gastoRepo;
        private readonly IDepositoRepository _depositoRepo;

        public GetMovimientosPorRangoQueryHandler(
            IRegistroGastoRepository gastoRepo,
            IDepositoRepository depositoRepo)
        {
            _gastoRepo = gastoRepo;
            _depositoRepo = depositoRepo;
        }

        public async Task<List<MovimientoDto>> Handle(GetMovimientosPorRangoQuery req, CancellationToken ct)
        {
            var gastos = await _gastoRepo.GetByRangoFechaAsync(req.Desde, req.Hasta, ct);
            var depositos = await _depositoRepo.GetByRangoFechaAsync(req.Desde, req.Hasta, ct);

            var movimientos = new List<MovimientoDto>();

            // Convertir gastos
            foreach (var r in gastos.Where(r => r.UsuarioId == req.UsuarioId))
            {
                foreach (var det in r.Detalles)
                {
                    movimientos.Add(new MovimientoDto
                    {
                        Id = r.Id,
                        Fecha = r.Fecha,
                        Tipo = MovimientoTipo.Gasto,
                        TipoGastoId = det.TipoGastoId,
                        Monto = det.Monto.Amount,
                        NombreComercio = r.NombreComercio,
                        TipoDocumento = r.TipoDocumento.ToString(),
                        FondoMonetarioId = r.FondoMonetarioId
                    });
                }
            }

            // Convertir depósitos
            foreach (var d in depositos.Where(d => d.UsuarioId == req.UsuarioId))
            {
                movimientos.Add(new MovimientoDto
                {
                    Id = d.Id,
                    Fecha = d.Fecha,
                    Tipo = MovimientoTipo.Deposito,
                    TipoGastoId = null,
                    Monto = d.Monto.Amount,
                    NombreComercio = null,
                    TipoDocumento = null,
                    FondoMonetarioId = d.FondoMonetarioId
                });
            }

            return movimientos
                .OrderBy(m => m.Fecha)
                .ToList();
        }
    }
}
