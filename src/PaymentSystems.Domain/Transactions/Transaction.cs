using System;
using PaymentSystems.FrameWork;
using static PaymentSystems.Domain.Transactions.TransactionEvents.V1;

namespace PaymentSystems.Domain.Transactions
{
    public class Transaction : Aggregate<TransactionId, TransactionState>
    {
        public void InitiateTransaction(TransactionId id, decimal amount, DateTimeOffset  submittedAt)
         {
            if (State.Status == TransactionStatus.Initiated) return;

            Apply(
                new TransactionInitiated
                {
                    TransactionId = State.TransactionId,
                    AccountId = State.AccountId,
                    Amount = State.Amount
                }
            );
         }

         public void BookTransaction()
         {
            if (State.Status != TransactionStatus.Initiated) return;

            Apply(
                new TransactionBooked
                {
                    TransactionId = State.TransactionId,
                    AccountId = State.AccountId,
                    Amount = State.Amount
                }
            );
         }

         public void CancelTransaction( string reason, DateTimeOffset  cancelledAt)
         {
            if (State.Status != TransactionStatus.Initiated) return;

            Apply(
                new TransactionDenied
                {
                    TransactionId = State.TransactionId,
                    AccountId = State.AccountId,
                    Reason = reason
                }
            );
         }

        public override TransactionState When(object evt) {
            return evt switch
            {
                TransactionInitiated e =>
                    new TransactionState
                    {
                        Id = new TransactionId(e.TransactionId),
                        AccountId = e.AccountId,
                        Amount = e.Amount,
                        Status = TransactionStatus.Initiated
                    },
                TransactionBooked => State = State with {Status = TransactionStatus.Booked},
                TransactionDenied => State = State with {Status = TransactionStatus.Denied},
                _ => State
            };
        }
    }
}

