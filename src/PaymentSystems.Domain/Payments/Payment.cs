using PaymentSystems.Domain.Events;

namespace PaymentSystems.Domain.Payments
{
    //Value objects to avoid invalid type arguments
    public class Payment
    {
         private string _reason;
         private decimal _amount;

         public void SubmitPayment(decimal amount)
         {

         }

         public void CancelPayment(decimal amount, string reason)
         {

         }

         public void ApprovePayment(decimal amount)
         {

         }

         public void ExecutePayment(decimal amount)
         {

         }

         public void RegisterPayment(decimal amount)
         {

         }

         public void ConfirmPayment(decimal amount)
         {

         }

         public void FailPayment(decimal amount)
         {

         }

         protected override void EnsureValidState() { }

        protected override void When(object @event) 
        {
            switch (@event) 
            {
                case PaymentEvents.V1.PaymentSubmitted e:
                    
                    e
                    
                    break;
               
            }
        }
    }
}