using BCrypt.Net;
using PruebaTecnica1.Core.Models.VOs;

namespace PruebaTecnica1.Application.Services
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        private const int WorkFactor = 10; // Ajustable (10–12 suele ser razonable)

        public string Hash(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(plainPassword));

            return BCrypt.Net.BCrypt.HashPassword(plainPassword, workFactor: WorkFactor);
        }

        public bool Verify(string plainPassword, PlainPassword passwordHash)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                return false;
            if (passwordHash is null)
                return false;

            string passwordHashString = passwordHash.ToString();
            return BCrypt.Net.BCrypt.Verify(plainPassword, passwordHashString);
        }
    }
}
