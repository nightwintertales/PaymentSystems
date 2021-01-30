namespace PaymentSystems.Domain.Accounts
{
     public record AccountDetails {
        public AccountDetails(string sortCode, string accountNumber) {
            SortCode      = sortCode;
            AccountNumber = accountNumber;
        }
        
        internal AccountDetails() { }

        public string   SortCode { get; init; }
        public string   AccountNumber      { get; init; }
    }

}