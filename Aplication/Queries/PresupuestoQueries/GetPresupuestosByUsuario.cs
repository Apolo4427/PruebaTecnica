using MediatR;
using PruebaTecnica1.Aplication.DTOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Queries.PresupuestoQueries
{
    public class GetPresupuestosByUsuarioAndMesQuery : IRequest<List<PresupuestoDto>>
    {
        public Guid UsuarioId { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
    }

    public class GetPresupuestosByUsuarioAndMesQueryHandler : IRequestHandler<GetPresupuestosByUsuarioAndMesQuery, List<PresupuestoDto>>
    {
        private readonly IPresupuestoRepository _repo;
        public GetPresupuestosByUsuarioAndMesQueryHandler(IPresupuestoRepository repo) => _repo = repo;

        public async Task<List<PresupuestoDto>> Handle(GetPresupuestosByUsuarioAndMesQuery req, CancellationToken ct)
        {
            var list = await _repo.GetByUsuarioAndMesAsync(req.UsuarioId, req.Anio, req.Mes, ct);
            return list
                .Select(p => new PresupuestoDto(
                    p.Id,
                    p.TipoGastoId,
                    p.Anio,
                    p.Mes,
                    p.Monto.Amount
                ))
                .ToList();
        }
    }
}