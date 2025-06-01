using MediatR;
using PruebaTecnica1.Aplication.DTOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Queries.TipoGastoQueries
{
    public record GetTipoGastoByIdQuery (
        Guid Id
    ) : IRequest<TipoGastoDto>;

    public class GetTipoGastoByIdQueryHandler : IRequestHandler<GetTipoGastoByIdQuery, TipoGastoDto>
    {
        private readonly ITipoGastoRepository _repo;
        public GetTipoGastoByIdQueryHandler(ITipoGastoRepository repo) => _repo = repo;

        public async Task<TipoGastoDto> Handle(GetTipoGastoByIdQuery req, CancellationToken ct)
        {
            var t = await _repo.GetByIdAsync(req.Id, ct);
            return new TipoGastoDto(
                t.Id,
                t.Codigo.ToString(),
                t.Nombre.ToString(),
                t.Descripcion
            );
        }
    }

}