namespace PruebaTecnica1.Core.Models.VOs
{
    public sealed class Money : ValueObject
    {
        public decimal Amount { get; }

        private Money(decimal amount) => Amount = amount;

        public static Money FromDecimal(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("El monto no puede ser negativo.", nameof(amount));
            return new Money(amount);
        }

        public Money Add(Money other) => FromDecimal(Amount + other.Amount);
        public Money Subtract(Money other)
        {
            var result = Amount - other.Amount;
            if (result < 0)
                throw new InvalidOperationException("Resultado de resta no puede ser negativo.");
            return FromDecimal(result);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
        }

        public override string ToString() => Amount.ToString("F2");
    }
}
