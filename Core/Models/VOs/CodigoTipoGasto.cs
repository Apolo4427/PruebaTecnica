using System.Text.RegularExpressions;

namespace PruebaTecnica1.Core.Models.VOs
{
    public sealed class CodigoTipoGasto : ValueObject
    {
        private static readonly Regex _regex = new(@"^TG-\d{4}$", RegexOptions.Compiled);
        public string Value { get; }

        private CodigoTipoGasto(string code) => Value = code;

        public static CodigoTipoGasto Create(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("El código no puede estar vacío.", nameof(code));
            if (!_regex.IsMatch(code))
                throw new ArgumentException("El código debe tener el formato TG-0001.", nameof(code));
            return new CodigoTipoGasto(code);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
