using PaymentSystems.Domain.Accounts;
using PaymentSystems.FrameWork;
using System;
using System.Collections.Generic;

namespace PaymentSystems.Domain.ScheduledPayments
{
    public class ScheduledPayment : Aggregate<ScheduledPaymentId, ScheduledPaymentState>
    {
        //new
        public void CreateScheduledPayments(AccountId accountId, ScheduledPaymentId scheduledPaymentId, IEnumerable<Repayment> payments, DateTimeOffset initiateAt)
        {
          //Raise events
        }

        public void UpdateScheduledPayments(AccountId accountId, ScheduledPaymentId scheduledPaymentId, IEnumerable<Repayment> payments, DateTimeOffset initiateAt)
        {
            //Raise events
        }

        public override ScheduledPaymentState When(object evt)
        {
            throw new System.NotImplementedException();
        }
    }
}