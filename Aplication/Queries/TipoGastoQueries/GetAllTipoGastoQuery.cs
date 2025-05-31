using MediatR;
using PruebaTecnica1.Core.Ports.Repositories;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Aplication.DTOs;

namespace PruebaTecnica1.Aplication.Queries.TipoGastoQueries
{
    public class GetAllTipoGastoQuery : IRequest<List<TipoGastoDto>> { }

    public class GetAllTipoGastoQueryHandler : IRequestHandler<GetAllTipoGastoQuery, List<TipoGastoDto>>
    {
        private readonly ITipoGastoRepository _repo;
        public GetAllTipoGastoQueryHandler(ITipoGastoRepository repo) => _repo = repo;

        public async Task<List<TipoGastoDto>> Handle(GetAllTipoGastoQuery req, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list
                .Select(t => new TipoGastoDto(
                    t.Id,
                    t.Codigo.ToString(),
                    t.Nombre.ToString(),
                    t.Descripcion
                ))
                .ToList();
        }
    }
}

