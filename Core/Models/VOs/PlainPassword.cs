namespace PruebaTecnica1.Core.Models.VOs
{
    public sealed class PlainPassword
    {
        public string Value { get; }

        private PlainPassword() {}

        private PlainPassword(string value) => Value = value;

        public static PlainPassword Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.", nameof(password));
            return new PlainPassword(password);
        }

        public override string ToString() => Value;
    }
}
