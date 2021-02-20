using System;

namespace PaymentSystems.Contract
{
    public static class AccountCommands
    {
         public record Initiate(
               string  AccountId,
                string  TransactionId,
                decimal Amount,
                DateTimeOffset InitiatedAt
            );

            public record Book(
                string  AccountId,
                string  TransactionId,
                string  ApprovedBy,
                DateTimeOffset BookedAt
            );
    }
}