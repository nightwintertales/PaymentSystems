using PaymentSystems.FrameWork;
namespace PaymentSystems.Domain.Transactions
{
     public record TransactionId(string Value) : AggregateId(Value) { }
}