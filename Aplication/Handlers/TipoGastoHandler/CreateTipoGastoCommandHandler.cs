using MediatR;
using PruebaTecnica1.Core.Ports.Repositories;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Aplication.Commands.TipoGastoCommand;

namespace PruebaTecnica1.Aplication.Handlers.TipoGastoHandler
{
    public class CreateTipoGastoCommandHandler : IRequestHandler<CreateTipoGastoCommand, Guid>
    {
        private readonly ITipoGastoRepository _repo;
        public CreateTipoGastoCommandHandler(ITipoGastoRepository repo) => _repo = repo;

        public async Task<Guid> Handle(CreateTipoGastoCommand req, CancellationToken ct)
        {
            // 1. Generar código:
            var nextCode = await _repo.GetNextCodigoAsync(ct);
            var codigoVo = CodigoTipoGasto.Create(nextCode);
            var nombreVo = Nombre.Create(req.Nombre);

            var entity = TipoGasto.Create(codigoVo, nombreVo, req.Descripcion);
            await _repo.AddAsync(entity, ct);
            return entity.Id;
        }
    }
}