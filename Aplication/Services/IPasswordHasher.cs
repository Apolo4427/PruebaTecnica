using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Application.Services
{
    public interface IPasswordHasher
    {
        /// <summary>
        /// Calcula el hash de la contraseña en texto plano.
        /// </summary>
        string Hash(string plainPassword);

        /// <summary>
        /// Verifica que el texto plano coincida con el hash almacenado.
        /// </summary>
        bool Verify(string plainPassword, PlainPassword passwordHash);
    }
}