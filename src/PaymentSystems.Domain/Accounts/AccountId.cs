using System;
using PaymentSystems.FrameWork;

namespace PaymentSystems.Domain.Accounts
{
    public record AccountId(string Value) : AggregateId(Value) { }

}