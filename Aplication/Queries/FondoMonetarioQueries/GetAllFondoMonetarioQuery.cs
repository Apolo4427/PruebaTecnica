using MediatR;
using PruebaTecnica1.Aplication.DTOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Queries.FondoMonetarioQueries
{
    public class GetAllFondosQuery : IRequest<List<FondoMonetarioDto>> { }

    public class GetAllFondosQueryHandler : IRequestHandler<GetAllFondosQuery, List<FondoMonetarioDto>>
    {
        private readonly IFondoMonetarioRepository _repo;
        public GetAllFondosQueryHandler(IFondoMonetarioRepository repo) => _repo = repo;

        public async Task<List<FondoMonetarioDto>> Handle(GetAllFondosQuery req, CancellationToken ct)
        {
            var list = await _repo.GetAllAsync(ct);
            return list
                .Select(f => new FondoMonetarioDto(
                    f.Id,
                    f.Nombre.ToString(),
                    f.Tipo,
                    f.Saldo.Amount
                ))
                .ToList();
        }
    }


}
