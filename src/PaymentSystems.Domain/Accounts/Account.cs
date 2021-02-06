using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentSystems.FrameWork;

namespace PaymentSystems.Domain.Accounts
{
    public class Account : Aggregate<AccountId, AccountState> 
    {
         public Account() => State = new AccountState();

         public void InitiateTransaction(Ammount amount, TransactionId transactionId, AccountId accountId)
         {
            Apply(
                new TransactionInitiated(
                            AccountId = accountId.Value,
                    TransactionId = transactionId.Value
                  
                )
            );
        }

         public void BookTransaction(Ammount amount, TransactionId transactionId, AccountId accountId)
         {
            Apply(
                new TransactionBooked(
                    
                    AccountId = accountId.Value,
                    TransactionId = transactionId.Value
                )
         }

         public void OpenAccount(AccountId id)
         {
            Apply(
                new AccountOpened
                {
                    AccountId = id.Value
                }
         }

         public override AccountState When(object evt)
            => evt switch {
                AccountOpened e =>
                    new AccountState {

                        Id = e.AccountId,
                        AvailableBalance = 0.0m,
                        DisposableAmount = 0.0m,
                        BookedAmount = 0.0m
                       
                    },
                TransactionBooked  e =>
                    State with {
                           Id = e.AccountId,
                        AvailableBalance = 0.0m,
                        DisposableAmount = 0.0m,
                        BookedAmount = 0.0m
                    },
                DiscountApplied e =>
                    State with {
                         Id = e.AccountId,
                         TransactionId = e.TransactionId,
                         AvailableBalance = State.AvailableBalance,
                         DisposableAmount = 0.0m,
                         BookedAmount = 0.0m
                    },
                _ => State
            };

    }
}