using System;

namespace PaymentSystems.Domain.Accounts {
    public static class AccountEvents {
        public static class V1 {
            public record AccountOpened(
                string AccountId,
                string CustomerId
            );

            public record TransactionInitiated(
                string  AccountId,
                string  TransactionId,
                decimal Amount,
                decimal AvailableBalance,
                DateTimeOffset InitiatedAt
            );

            public record TransactionBooked(
                string  AccountId,
                string  TransactionId,
                decimal BookedBalance,
                DateTimeOffset BookedAt
            );
            
            public record TransactionCancelled(
                string  AccountId,
                string  TransactionId,
                decimal AvailableBalance,
                string Reason,
                DateTimeOffset DeniedAt
            );
        }
    }
}
