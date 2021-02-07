using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentSystems.FrameWork;
using PaymentSystems.Domain.Accounts;
using static PaymentSystems.Domain.Events.PaymentEvents.V1;
using PaymentSystems.Domain.Transactions;

namespace PaymentSystems.Domain.Payments
{
    //Consider Payment Status that changes with different events
    public class Payment : Aggregate<PaymentId, PaymentState> 
    {
         public Payment() => State = new PaymentState();

         public void SubmitPayment(AccountId accountId, PaymentId paymentId, decimal amount)
         {
            Apply(
                new PaymentSubmitted()
                {
                    AccountId = accountId.Value,
                    Amount = amount,
                    PaymentId = paymentId.Value
                }
            );
        }

         public void ApprovePayment(decimal amount, AccountId accountId)
         {
            Apply(
                new PaymentApproved()
                {
                    AccountId = accountId.Value,
                    Amount = amount,
                    PaymentId = State.PaymentId
                }
            );
         }

         public void ExecutePayment(decimal amount, AccountId accountId)
         {
            Apply(
                new PaymentExecuted()
                {
                    
                    AccountId = accountId.Value,
                    Amount = amount,
                    PaymentId = State.PaymentId
                }
            );
         }

         public override PaymentState When(object evt)
            => evt switch {
                PaymentSubmitted e =>
                    new PaymentState {

                          
                       
                    },
                PaymentApproved  e => 
                 State = new PaymentState()
                 {

                 },
                PaymentExecuted  e => 
                 State = new PaymentState()
                 {
                     
                 }
              
            };

    }
}