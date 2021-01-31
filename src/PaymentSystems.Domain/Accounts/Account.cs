using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentSystems.FrameWork;

namespace PaymentSystems.Domain.Accounts
{
    public class Account : AggregateRoot<AccountId> {
        private string _reason;
         private decimal _amount;
         private string _transactionId;
         private string _AccountId;

         public void InitiateTransaction(decimal amount)
         {

         }

         public void BookTransaction(decimal amount, string reason)
         {

         }

         public void CancelTransaction(decimal amount)
         {

         }

        protected override void EnsureValidState() { }

        protected override void When(object @event) 
        {
            switch (@event) 
            {
            
               
            }
        }
    }
}