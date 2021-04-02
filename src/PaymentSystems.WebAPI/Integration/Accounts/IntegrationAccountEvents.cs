using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSystems.WebAPI.Integration.Accounts
{
    public static class IntegrationAccountEvents
    {
        // How are we going to obtain PaymentId
        public static class V1
        {
            public record AccountOpened(
                string AccountId,
                string CustomerId,
                string PaymentId,
                decimal Balance,
                DateTimeOffset OpenedAt
            );

            public record TransactionInitiated(
                string AccountId,
                string TransactionId,
                decimal Amount,
                decimal AvailableBalance,
                string PaymentId,
                DateTimeOffset InitiatedAt
            );

            public record TransactionBooked(
                string AccountId,
                string TransactionId,
                decimal BookedBalance,
                string PaymentId,
                DateTimeOffset BookedAt
            );
        }
    }
}
