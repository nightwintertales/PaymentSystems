namespace PaymentSystems.Contract
{
    public static class Commands
    {
        public static class Accounts
        {
            public record CreateAccount(string AccountId, string FirstName, string LastName);

            public record CreateAddress(string AccountId, string FirstLine, string PostCode, string City, string County);

            public record MakePayment(string AccountId, decimal Ammount, string DebtorSortCode, string DebtorAccountNumber);
        }
    }
}
