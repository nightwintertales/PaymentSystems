using System;
using Eventuous;
using PaymentSystems.Domain.Accounts;
using static PaymentSystems.Domain.Transactions.TransactionEvents.V1;

namespace PaymentSystems.Domain.Transactions {
    public class Transaction : Aggregate<TransactionState,TransactionId> {
        public void InitiateTransaction(
            TransactionId id, AccountId accountId, decimal amount, DateTimeOffset initiatedAt
        ) {
            if (State.Status == TransactionStatus.Initiated) return;

            Apply(
                new TransactionInitiated {
                    TransactionId = id,
                    AccountId     = accountId,
                    Amount        = amount,
                    InitiatedAt   = initiatedAt
                }
            );
        }

        public void BookTransaction(DateTimeOffset bookedAt) {
            if (State.Status != TransactionStatus.Initiated) return;

            Apply(
                new TransactionBooked {
                    TransactionId = State.TransactionId,
                    AccountId     = State.AccountId,
                    BookedAt      = bookedAt
                }
            );
        }

        public void CancelTransaction(string reason, DateTimeOffset cancelledAt) {
            if (State.Status != TransactionStatus.Initiated) return;

            Apply(
                new TransactionDenied {
                    TransactionId = State.TransactionId,
                    AccountId     = State.AccountId,
                    Reason        = reason,
                    DeniedAt      = cancelledAt
                }
            );
        }

        /*
        public override TransactionState When(object evt) {
            return evt switch {
                TransactionInitiated e =>
                    new TransactionState {
                        Id        = new TransactionId(e.TransactionId),
                        AccountId = e.AccountId,
                        Amount    = e.Amount,
                        Status    = TransactionStatus.Initiated
                    },
                TransactionBooked => State = State with {Status = TransactionStatus.Booked},
                TransactionDenied => State = State with {Status = TransactionStatus.Denied},
                _ => State
            };
        }
        
        */
    }
}