using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentSystems.FrameWork;

namespace PaymentSystems.Domain.Accounts
{
    public class Account : AggregateRoot<AccountId> 
    {
        //Or else use State
         private string _reason;
         private decimal _availableBalance;
         private decimal _availableBalance;
         private decimal _disposableAmount;
         private decimal _bookedAmount;
         private string _transactionId;
         private string _accountId;

         public void InitiateTransaction(Ammount amount, TransactionId transactionId)
         {

         }

         public void BookTransaction(Ammount amount, TransactionId transactionId, string reason)
         {

         }

         public void OpenAccount(AccountId id)
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