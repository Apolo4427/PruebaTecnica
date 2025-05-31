using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Aplication.Commands.FondoMonetarioCommand;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Handlers.FondoMonetarioHandler
{
    public class UpdateFondoMonetarioCommandHandler : IRequestHandler<UpdateFondoMonetarioCommand>
    {
        private readonly IFondoMonetarioRepository _repo;
        public UpdateFondoMonetarioCommandHandler(IFondoMonetarioRepository repo) => _repo = repo;

        public async Task Handle(UpdateFondoMonetarioCommand req, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(req.Id, ct)
                ?? throw new KeyNotFoundException(
                    $"El Fondo monetario con id: {req.Id} no ha sido encontrad"
                );
            entity.CambiarNombre(Nombre.Create(req.Nombre));
            entity.CambiarTipo(req.Tipo);
            try
            {
                await _repo.UpdateAsync(entity, ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DBConcurrencyException(
                    $"El fondo monetario con id {req.Id} ya ha sido modificado por otro proceso"
                );
            }
        }
    }
}