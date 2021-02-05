namespace PaymentSystems.Domain.Payments
{
    public record PaymentId
    {
        public Guid Value { get; }

        public PaymentId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "PaymentId cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(PaymentId self) => self.Value;

        public static implicit operator PaymentId(string value)
            => new(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}