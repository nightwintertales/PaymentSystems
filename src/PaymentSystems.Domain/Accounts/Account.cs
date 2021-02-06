using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentSystems.FrameWork;
using static PaymentSystems.Domain.Events.AccountEvents.V1;
using PaymentSystems.Domain.Transactions;

namespace PaymentSystems.Domain.Accounts
{
    public class Account : Aggregate<AccountId, AccountState> 
    {
         public Account() => State = new AccountState();

         public void InitiateTransaction(decimal amount, TransactionId transactionId, AccountId accountId)
         {
            Apply(
                new TransactionInitiated()
                {
                    AccountId = accountId.Value,
                    TransactionId = transactionId.Value,
                    BookedAmount = State.AvailableBalance = amount,
                    //NOT SURE THISIS CORRECT THOUGH - CAn we set event properties by using Aggregate State ?
                    DisposableAmount = amount,
                    AvailableBalance = State.AvailableBalance
                }
            );
        }

         public void BookTransaction(decimal amount, TransactionId transactionId, AccountId accountId)
         {
            Apply(
                new TransactionBooked()
                {
                    
                    AccountId = accountId.Value,
                    TransactionId = transactionId.Value,
                    BookedAmount = State.AvailableBalance = amount,
                    //NOT SURE THISIS CORRECT THOUGH - CAn we set event properties by using Aggregate State ?
                    DisposableAmount = amount,
                    AvailableBalance = State.AvailableBalance
                }
            );
         }

         public void OpenAccount(AccountId accountId)
         {
             Apply(
                new AccountOpened()
                {
                    AccountId = accountId.Value
                }
            );
         }

         public override AccountState When(object evt)
            => evt switch {
                AccountOpened e =>
                    new AccountState {

                        //d = e.AccountId,
                        AvailableBalance = 10000m,
                        TransactionAmount = 0.0m,
                        BookedAmount = 0.0m
                       
                    },
                TransactionInitiated  e => 
                 State = new AccountState()
                 {

                 },
                TransactionBooked  e => 
                 State = new AccountState()
                 {
                     
                 }
              
            };

    }
}