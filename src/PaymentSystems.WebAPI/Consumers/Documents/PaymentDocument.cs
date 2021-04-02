using PaymentSystems.FrameWork;
using System.Collections.Generic;

namespace PaymentSystems.WebAPI.Consumers.Documents
{
    public record PaymentDocument : Document
    {
        public string PaymentId { get; init; }

        public decimal PaymentAmount { get; init; }

        public List<Payment> Payments { get; init; } = new();
    }

    public record Payment(
      string PaymentId, decimal PaymentAmount);
}
