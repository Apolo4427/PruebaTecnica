using Microsoft.EntityFrameworkCore;
using PruebaTecnica1.Core.Models;
using PruebaTecnica1.Core.Models.VOs;
using PruebaTecnica1.Core.Ports.Repositories;
using PruebaTecnica1.Interface.Persistence.Data;

namespace PruebaTecnica1.Interface.Persistence.Repositories
{
    public class UsuarioEfRepository : IUsuarioRepository
    {
        private readonly AppDbContext _db;
        public UsuarioEfRepository(AppDbContext db) => _db = db;

        public async Task<Usuario> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(
                u => u.NombreUsuario == Username.Create(username),
                cancellationToken
            );

            if (usuario is null)
                throw new KeyNotFoundException(
                    $"El usuario con nombre {username} no ha sido encontrado"
                );
                
            return usuario;
        }

        public async Task AddAsync(Usuario entity, CancellationToken cancellationToken = default)
        {
            _db.Usuarios.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}