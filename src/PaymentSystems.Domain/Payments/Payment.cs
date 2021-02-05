using PaymentSystems.Domain.Events;

namespace PaymentSystems.Domain.Payments
{
    //Value objects to avoid invalid type arguments
    public class Payment : AggregateRoot<PaymentId> 
    {
         public void SubmitPayment(PaymentId paymentId, Amount amount, DateTimeOffset  submittedAt)
         {

         }

         public void ApprovePayment(Amount amount)
         {

         }

         public void ExecutePayment(Amount amount)
         {

         }

         protected override void When(object @event) 
        {
            switch (@event) 
            {
            
               
            }
        }
    }
}