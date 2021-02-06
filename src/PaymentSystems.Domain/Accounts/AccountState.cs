using PaymentSystems.FrameWork;
using PaymentSystems.Domain.Transactions;

namespace PaymentSystems.Domain.Accounts
{
    public class AccountState : AggregateState<AccountId> 
    {
        //Typically it is the ending balance on the bank statement for each month - Not sure if this has to be in the aggregate. It looks like a field that is calculated on the fly in read model??
         public decimal AvailableBalance { get; set; }
         public decimal TransactionAmount { get; set; }
         //Book balance is a banking term used to describe funds on deposit after adjustments
         public decimal BookedAmount { get; set; }
         public TransactionId TransactionId { get; set; }
}
    }