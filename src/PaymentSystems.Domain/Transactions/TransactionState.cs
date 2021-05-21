using Eventuous;

namespace PaymentSystems.Domain.Transactions
{
    public record TransactionState : AggregateState<TransactionState,TransactionId> 
    {
        public string  AccountId     { get; set; }
        public string  TransactionId { get; set; }
        public decimal Amount {get;set;}
        public string Reason {get;set;}
        public TransactionStatus Status {get;set;}
        public TransactionState State { get; set; }
        public override TransactionState When(object @event) =>
            @event switch {
                TransactionEvents.V1.TransactionInitiated e =>
                    new TransactionState {
                        Id        = new TransactionId(e.TransactionId),
                        AccountId = e.AccountId,
                        Amount    = e.Amount,
                        Status    = TransactionStatus.Initiated
                    },
                TransactionEvents.V1.TransactionBooked => State = State with {Status = TransactionStatus.Booked},
                TransactionEvents.V1.TransactionDenied => State = State with {Status = TransactionStatus.Denied},
                _ => State
            };
    }
}