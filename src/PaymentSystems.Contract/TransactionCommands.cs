using System;

namespace PaymentSystems.Contract
{
    public static class TransactionCommands
    {
        public record InitiateTransaction(
               string  AccountId,
                string  TransactionId,
                decimal Amount,
                DateTimeOffset InitiatedAt
            );

            public record BookTransaction(
                string  AccountId,
                string  TransactionId,
                DateTimeOffset BookedAt
            );
            
            public record CancelTransaction(
                string  AccountId,
                string  TransactionId,
                string Reason,
                DateTimeOffset CancelledAt
            );
    }
}