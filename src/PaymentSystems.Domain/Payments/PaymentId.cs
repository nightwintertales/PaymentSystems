using System;
using PaymentSystems.FrameWork;
namespace PaymentSystems.Domain.Payments
{
      public record PaymentId(string Value) : AggregateId(Value) { }
}