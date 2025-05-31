namespace PruebaTecnica1.Core.Models.VOs
{
    public sealed class Nombre : ValueObject
    {
        public string Value { get; }

        private Nombre(string value) => Value = value;

        public static Nombre Create(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            if (nombre.Length > 100)
                throw new ArgumentException("El nombre no puede exceder 100 caracteres.", nameof(nombre));
            return new Nombre(nombre.Trim());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
