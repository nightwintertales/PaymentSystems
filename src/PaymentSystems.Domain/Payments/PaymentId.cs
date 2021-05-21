using Eventuous;

namespace PaymentSystems.Domain.Payments
{
      public record PaymentId(string Value) : AggregateId(Value) { }
}