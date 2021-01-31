using PaymentSystems.Domain.Events;

namespace PaymentSystems.Domain.Payments
{
    //Value objects to avoid invalid type arguments
    public class Payment
    {
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

    }
}