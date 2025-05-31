using MediatR;
using PruebaTecnica1.Aplication.Commands.FondoMonetarioCommand;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Handlers.FondoMonetarioHandler
{
    public class CreateFondoMonetarioCommandHandler : IRequestHandler<CreateFondoMonetarioCommand, Guid>
    {
        private readonly IFondoMonetarioRepository _repo;
        public CreateFondoMonetarioCommandHandler(IFondoMonetarioRepository repo) => _repo = repo;

        public async Task<Guid> Handle(CreateFondoMonetarioCommand req, CancellationToken ct)
        {
            var nombreVo = Nombre.Create(req.Nombre);
            var saldoVo = Money.FromDecimal(req.SaldoInicial);
            var entity = FondoMonetario.Create(nombreVo, req.Tipo, saldoVo);
            await _repo.AddAsync(entity, ct);
            return entity.Id;
        }
    }
}