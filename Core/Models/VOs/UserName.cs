using System.Text.RegularExpressions;

namespace PruebaTecnica1.Core.Models.VOs
{
    public sealed class Username : ValueObject
    {
        private static readonly Regex _allowed = new(@"^[a-zA-Z0-9_]{3,20}$", RegexOptions.Compiled);
        public string Value { get; }

        private Username() {}
        private Username(string value)
        {
            Value = value;
        }

        public static Username Create(string user)
        {
            if (string.IsNullOrWhiteSpace(user))
                throw new ArgumentException("El nombre de usuario es obligatorio.", nameof(user));

            if (!_allowed.IsMatch(user))
                throw new ArgumentException(
                    "El nombre de usuario debe tener entre 3 y 20 caracteres y solo puede contener letras, números y guiones bajos.",
                    nameof(user));

            return new Username(user);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
