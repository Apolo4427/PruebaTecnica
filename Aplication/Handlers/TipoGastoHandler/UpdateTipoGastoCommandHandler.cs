using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Aplication.Commands.TipoGastoCommand;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Handlers.TipoGastoHandler
{
    public class UpdateTipoGastoCommandHandler : IRequestHandler<UpdateTipoGastoCommand>
    {
        private readonly ITipoGastoRepository _repo;
        public UpdateTipoGastoCommandHandler(ITipoGastoRepository repo) => _repo = repo;

        public async Task Handle(UpdateTipoGastoCommand req, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(req.Id, ct)
                ?? throw new KeyNotFoundException(
                    $"El tipo de gasto con id {req.Id} no ha sido encontrado"
                );
            try
            {
                entity.Update(Nombre.Create(req.Nombre), req.Descripcion);
                await _repo.UpdateAsync(entity, ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DBConcurrencyException($"El objeto con id {req.Id} ya ha sido modificado por otro proceso");
            }
        }
    }
}