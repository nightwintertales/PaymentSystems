namespace PaymentSystems.Domain.Accounts
{
    public class AccountId : Value<AccountId>
    {
        public Guid Value { get; }

        public AccountId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "AccountId cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(AccountId self) => self.Value;

        public static implicit operator AccountId(string value)
            => new AccountId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }

}