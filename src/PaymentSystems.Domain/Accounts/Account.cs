using System;
using Eventuous;
using PaymentSystems.Domain.Payments;
using static PaymentSystems.Domain.Accounts.AccountEvents.V1;
using PaymentSystems.Domain.Transactions;

namespace PaymentSystems.Domain.Accounts {
    public class Account : Aggregate<AccountState,AccountId> {
       // public Account() => State = new AccountState();

        public void OpenAccount(AccountId accountId, string CustomerId) {
            Apply(
                new AccountOpened(accountId, CustomerId)
            );
        }

        public void InitiateTransaction(TransactionId transactionId, decimal amount, DateTimeOffset at) {
            if (State.PendingTransactions.HasTransaction(transactionId)) return;

            if (State.BookedTransactions.HasTransaction(transactionId)) {
                // initiating a transaction, which is already booked??? weird!
                return;
            }

            Apply(
                new TransactionInitiated(
                    State.Id,
                    transactionId,
                    State.AvailableBalance - amount,
                    amount,
                    at
                )
            );
        }

        public void BookTransaction(TransactionId transactionId, DateTimeOffset at) {
            var transaction = State.PendingTransactions.FindTransaction(transactionId);
            if (transaction == default) throw new Exception("Transaction unknown");

            if (State.BookedTransactions.HasTransaction(transactionId)) return;

            Apply(
                new TransactionBooked(
                    State.Id,
                    transactionId,
                    State.AccountBalance - transaction.Amount,
                    at
                )
            );
        }

        public void CancelTransaction(TransactionId transactionId, string reason, DateTimeOffset at) {
            // if (State.InitiatedTransactions.HasTransaction(transactionId)) return;

            var transaction = State.PendingTransactions.FindTransaction(transactionId);
            if (transaction == default) return;
            // Idempotence can be ensured differently. Either keep it in a list, or just ignore

            Apply(
                new TransactionCancelled(
                    State.Id,
                    transactionId,
                    State.AvailableBalance + transaction.Amount,
                    reason,
                    at
                )
            );
        }

        public bool CanExecutePayment(Payment payment) {
            return State.AvailableBalance - payment.State.Amount >= 0;
        }

        /*
        public override AccountState When(object evt) {
            return evt switch {
                AccountOpened e =>
                    new AccountState {
                        Id               = new AccountId(e.AccountId),
                        AvailableBalance = 10000,
                        AccountBalance   = 10000
                    },
                TransactionInitiated e => State = State.Handle(e),
                TransactionBooked e    => State = State.Handle(e),
                TransactionCancelled e => State = State.Handle(e),
                _                      => State
            };
        }
        */
    }
}