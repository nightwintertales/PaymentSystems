namespace PaymentSystems.Domain.Transactions
{
     public record TransactionId
    {
        public Guid Value { get; }

        public TransactionId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "TransactionId cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(TransactionId self) => self.Value;

        public static implicit operator TransactionId(string value)
            => new(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}