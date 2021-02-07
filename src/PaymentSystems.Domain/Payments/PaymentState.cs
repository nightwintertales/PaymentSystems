using PaymentSystems.FrameWork;

namespace PaymentSystems.Domain.Payments
{
    public class PaymentState : AggregateState<PaymentId> 
    {
        public string  AccountId     { get; set; }
        public string  PaymentId { get; set; }
        public decimal Amount {get;set;}
        public decimal Name   { get; set; }
        public PaymentStatus Status {get;set;}
    }
}