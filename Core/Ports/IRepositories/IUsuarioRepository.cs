using PruebaTecnica1.Core.Models;

namespace PruebaTecnica1.Core.Ports.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task AddAsync(Usuario entity, CancellationToken cancellationToken = default);
    }
}
