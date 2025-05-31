using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Aplication.Commands.FondoMonetarioCommand;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Handlers.FondoMonetarioHandler
{
    public class DeleteFondoMonetarioCommandHandler : IRequestHandler<DeleteFondoMonetarioCommand>
    {
        private readonly IFondoMonetarioRepository _repo;
        public DeleteFondoMonetarioCommandHandler(IFondoMonetarioRepository repo) => _repo = repo;

        public async Task Handle(DeleteFondoMonetarioCommand req, CancellationToken ct)
        {
            try
            {
                await _repo.DeleteAsync(req.Id, ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DBConcurrencyException(
                    $"El fondo monetario con id {req.Id} ya ha sido eliminado en otro proceso"
                );
            }
        }
    }
}