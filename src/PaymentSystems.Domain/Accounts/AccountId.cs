using Eventuous;

namespace PaymentSystems.Domain.Accounts
{
    public record AccountId(string Value) : AggregateId(Value) { }

}