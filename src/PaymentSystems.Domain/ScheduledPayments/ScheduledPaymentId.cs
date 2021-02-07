using PaymentSystems.FrameWork;

namespace PaymentSystems.Domain.ScheduledPayments
{
     public record ScheduledPaymentId(string Value) : AggregateId(Value) { }
}