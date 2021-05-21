using Eventuous;
using PaymentSystems.Domain.Events;

namespace PaymentSystems.Domain.Payments
{
    public record PaymentState : AggregateState<PaymentState,PaymentId>
    {
        public string  AccountId     { get; set; }
        public string  PaymentId { get; set; }
        public decimal Amount {get;set;}
        public decimal Name   { get; set; }
        public PaymentStatus Status {get;set;}

        public PaymentState State { get; set; }

        public override PaymentState When(object @event)
            => @event switch
            {
                PaymentEvents.V1.PaymentSubmitted e =>
                    new PaymentState
                    {
                        AccountId = e.AccountId,
                        Amount = e.Amount,
                        Status = PaymentStatus.Submitted,
                        PaymentId = e.PaymentId
                    },
                PaymentEvents.V1.PaymentApproved => State = State with { Status = PaymentStatus.Approved},
                PaymentEvents.V1.PaymentExecuted => State = State with {Status = PaymentStatus.Executed},
                _ => this
            };
    }
}