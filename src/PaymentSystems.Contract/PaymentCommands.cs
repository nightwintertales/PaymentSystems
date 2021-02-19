using System;
namespace PaymentSystems.Contract
{
    public static class PaymentCommands {
            public record Submit(
                string  AccountId,
                decimal Amount,
                string  PaymentId,
                PayeeAccount payeeAccount,
                DateTimeOffset SubmittedAt
            );

            public record Approve(
                string  PaymentId,
                string  ApprovedBy,
                DateTimeOffset ApprovedAt
            );

            public record Execute(
                string  AccountId,
                string  PaymentId,
                DateTimeOffset ExecutedAt
            );
        }
}
