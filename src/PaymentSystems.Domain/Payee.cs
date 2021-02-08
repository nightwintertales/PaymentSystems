namespace PaymentSystems.Domain {
    public record Payee {
        public string Name { get; }

        public PayeeAccount Account { get; }

        public Payee(string name, PayeeAccount account) {
            Name = name;
            Account = account;
        }
    }

    public record PayeeAccount {
        public string AccountNumber { get; }

        public string SortCode { get; }

        public PayeeAccount(string accountNumber, string sortCode) {
            // check for empty
            
            AccountNumber = accountNumber;
            SortCode = sortCode;
        }
    }
}