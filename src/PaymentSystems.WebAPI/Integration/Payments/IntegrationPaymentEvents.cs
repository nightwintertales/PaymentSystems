using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSystems.WebAPI.Integration.Payments
{
    public class IntegrationPaymentEvents
    {
        public static class V1
        {
            public record PaymentApproved(
                string AccountId,
                string CustomerId,
                string PaymentId,
                decimal Balance,
                DateTimeOffset OpenedAt
            );
        }
    }
}
