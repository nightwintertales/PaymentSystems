using System;
namespace PaymentSystems.Contract
{
    public static class PaymentCommands {
            public record Submit(
                string  AccountId,
                decimal Amount,
                string  PaymentId,
                DateTimeOffset SubmittedAt
            );

            public record Approve(
                string  AccountId,
                decimal Amount,
                string  PaymentId,
                DateTimeOffset ApprovedAt
            );

            public record Execute(
                string  AccountId,
                decimal Amount,
                string  PaymentId,
                DateTimeOffset ExecutedAt
            );
        }
    }

