using MediatR;
using PruebaTecnica1.Aplication.Commands.DepositoCommnad;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Core.Ports.Repositories;

namespace PruebaTecnica1.Aplication.Handlers.DepositoHandler
{
    public class CreateDepositoCommandHandler : IRequestHandler<CreateDepositoCommand, Guid>
    {
        private readonly IDepositoRepository _repo;
        private readonly IFondoMonetarioRepository _fondoRepo;
        public CreateDepositoCommandHandler(
            IDepositoRepository repo,
            IFondoMonetarioRepository fondoRepo)
        {
            _repo = repo;
            _fondoRepo = fondoRepo;
        }

        public async Task<Guid> Handle(CreateDepositoCommand req, CancellationToken ct)
        {
            // 1. Crear entidad Deposito
            var deposito = Deposito.Create(
                req.UsuarioId,
                req.FondoMonetarioId,
                req.Fecha,
                Money.FromDecimal(req.Monto)
            );

            // 2. Ajustar saldo en fondo
            await _fondoRepo.AjustarSaldoAsync(req.FondoMonetarioId, req.Monto, true, ct);

            // 3. Guardar depósito
            await _repo.AddAsync(deposito, ct);
            return deposito.Id;
        }
    }
}