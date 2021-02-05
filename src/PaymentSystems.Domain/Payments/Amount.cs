namespace PaymentSystems.Domain.Payments
{
     public record Amount {
        public float  Amount   { get; internal init; }
        public string Currency { get; internal init; }

        static readonly string[] SupportedCurrencies = {"USD", "EUR", "GPB"};

        internal Money() { }

        public Money(float amount, string currency) {
            if (!SupportedCurrencies.Contains(currency)) throw new DomainException($"Unsupported currency: {currency}");

            if(amount<= 0)
            throw new DomainException($"Amount needs to be greater than 0");

            Amount   = amount;
            Currency = currency;
        }

        public bool IsSameCurrency(Money another) => Currency == another.Currency;

        public static Money operator -(Money one, Money another) {
            if (!one.IsSameCurrency(another)) throw new DomainException("Cannot operate on different currencies");

            return new Money(one.Amount - another.Amount, one.Currency);
        }

        public static implicit operator double(Money money) => money.Amount;
    }

}