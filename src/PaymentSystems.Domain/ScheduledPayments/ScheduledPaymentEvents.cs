using System;
using System.Collections.Generic;

namespace PaymentSystems.Domain.ScheduledPayments
{
    public static class ScheduledPaymentEvents {
        public static class V1
        {
            public record ScheduledDebitPlanCreated(
            string AccountId,
            string RescheduledPaymentId,
            IEnumerable<Repayment> Payments,
            DateTimeOffset CreatedAt);

           public record ScheduledDebitPlanUpdatedd(
           string AccountId,
           string RescheduledPaymentId,
           IEnumerable<Repayment> Payments,
           DateTimeOffset CreatedAt);
        }
    }
}