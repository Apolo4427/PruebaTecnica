using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Aplication.Commands.TipoGastoCommand;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Handlers.TipoGastoHandler
{
    public class DeleteTipoGastoCommandHandler : IRequestHandler<DeleteTipoGastoCommand>
    {
        private readonly ITipoGastoRepository _repo;
        public DeleteTipoGastoCommandHandler(ITipoGastoRepository repo) => _repo = repo;

        public async Task Handle(DeleteTipoGastoCommand req, CancellationToken ct)
        {
            try
            {
                await _repo.DeleteAsync(req.Id, ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DBConcurrencyException(
                    $"El tipo de gasto de id: {req.Id} ya ha sido eleiminado por otro proceso"
                );
            }
        }
    }
}