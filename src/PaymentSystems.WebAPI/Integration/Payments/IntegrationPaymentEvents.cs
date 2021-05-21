using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSystems.WebAPI.Integration.Payments {
    public static class IntegrationPaymentEvents {
        public static class V1 {
            public record PaymentApproved(
                string         AccountId,
                string         PaymentId,
                decimal        PaymentAmount,
                Payee          Payee,
                DateTimeOffset ApprovedAt,
                string         ApprovedBy
            );

            public record Payee(string Name, string SortCode, string AccountNumber);
        }
    }
}