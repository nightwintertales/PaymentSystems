namespace PaymentSystems.Domain.Accounts
{
    public class AccountState : AggregateState<AccountId> {
    {
         public decimal AvailableBalance { get; set; }
         public decimal DisposableAmount { get; set; }
         public decimal BookedAmount { get; set; }
         public TransactionId TransactionId { get; set; }
    }
}