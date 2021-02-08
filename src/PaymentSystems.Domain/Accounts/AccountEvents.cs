namespace PaymentSystems.Domain.Accounts {
    public static class AccountEvents {
        public static class V1 {
            public record TransactionInitiated(
                string  AccountId,
                string  TransactionId,
                decimal Amount,
                decimal AvailableBalance
            );

            public class TransactionBooked {
                public string AccountId { get; set; }

                public string TransactionId { get; set; }

                public decimal BookedAmount { get; set; }
            }

            public class AccountOpened {
                public string AccountId { get; set; }

                public string CustomerId { get; set; }
            }
        }
    }
}