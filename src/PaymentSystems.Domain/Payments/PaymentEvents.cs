using System;
using PaymentSystems.Domain.Payments;

namespace PaymentSystems.Domain.Events {
    public static class PaymentEvents {
        public static class V1 {
            public class PaymentSubmitted {
                public string AccountId { get; set; }

                public string PaymentId { get; set; }

                public decimal Amount { get; set; }
                
                public Shared.Shared.V1.CounterParty Payee { get; init; }
                
                public string Message { get; init; }
                
                public string SubmittedBy { get; init; }
                
                public DateTimeOffset SubmittedAt { get; init; }
            }

            public class PaymentApproved {
                public string PaymentId { get; init; }
                public string ApprovedBy { get; init; }
                public DateTimeOffset ApprovedAt { get; init; }
            }

            public class PaymentExecuted {
                public string PaymentId { get; set; }
                
                public DateTimeOffset ExecutedAt { get; init; }
            }

            public record PaymentDenied(string PaymentId, string Reason, DateTimeOffset DeniedAt);
        }
    }
}