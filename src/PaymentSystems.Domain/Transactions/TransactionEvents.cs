using System;

namespace PaymentSystems.Domain.Transactions {
    public static class TransactionEvents {
        public static class V1 {
            public class TransactionInitiated {
                public string TransactionId { get; set; }

                // Good point, what kind of payment are we dealing with?
                //public string PaymentId {get;set;}
                public string AccountId { get; set; }

                public decimal Amount { get; set; }
                
                public DateTimeOffset InitiatedAt { get; init; }
            }

            public class TransactionBooked {
                public string TransactionId { get; set; }

                // public string PaymentId {get;set;}
                public string AccountId { get; set; }
                
                public DateTimeOffset BookedAt { get; init; }
            }

            public class TransactionDenied {
                public string TransactionId { get; set; }

                // public string PaymentId {get;set;}
                public string AccountId { get; set; }

                public string Reason { get; set; }
                
                public DateTimeOffset DeniedAt { get; init; }
            }
        }
    }
}