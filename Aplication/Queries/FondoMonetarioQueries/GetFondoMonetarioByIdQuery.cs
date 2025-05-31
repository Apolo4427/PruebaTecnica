using MediatR;
using PruebaTecnica1.Aplication.DTOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Queries.FondoMonetarioQueries
{
    public class GetFondoByIdQuery : IRequest<FondoMonetarioDto>
    {
        public Guid Id { get; set; }
    }

    public class GetFondoByIdQueryHandler : IRequestHandler<GetFondoByIdQuery, FondoMonetarioDto>
    {
        private readonly IFondoMonetarioRepository _repo;
        public GetFondoByIdQueryHandler(IFondoMonetarioRepository repo) => _repo = repo;

        public async Task<FondoMonetarioDto> Handle(GetFondoByIdQuery req, CancellationToken ct)
        {
            var f = await _repo.GetByIdAsync(req.Id, ct);
            return new FondoMonetarioDto(
                f.Id,
                f.Nombre.ToString(),
                f.Tipo,
                f.Saldo.Amount
            );
        }
    }
}