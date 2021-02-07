using PaymentSystems.FrameWork;

namespace PaymentSystems.Domain.ScheduledPayments
{
    public class ScheduledPayment : Aggregate<ScheduledPaymentId, ScheduledPaymentState>
    {
        
        public override ScheduledPaymentState When(object evt)
        {
            throw new System.NotImplementedException();
        }
    }
}