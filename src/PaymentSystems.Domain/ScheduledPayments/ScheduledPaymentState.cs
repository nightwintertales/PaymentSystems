using PaymentSystems.FrameWork;
using System;
using System.Collections.Generic;

namespace PaymentSystems.Domain.ScheduledPayments
{
    public record ScheduledPaymentState : AggregateState<ScheduledPaymentId>
    {
        public IEnumerable<Repayment> Repayments { get; set; }
    }


    public class Repayment
    { 
        public DateTimeOffset Date { get; set; }
        public decimal Amount { get; set; }
    }
}