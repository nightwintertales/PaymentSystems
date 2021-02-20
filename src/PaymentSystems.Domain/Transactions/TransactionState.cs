using PaymentSystems.FrameWork;

namespace PaymentSystems.Domain.Transactions
{
    public record TransactionState : AggregateState<TransactionId> 
    {
        public string  AccountId     { get; set; }
        public string  TransactionId { get; set; }
        public decimal Amount {get;set;}
        public string Reason {get;set;}
        public TransactionStatus Status {get;set;}
    }
}