using System.Collections.Generic;
using Eventuous;
using PaymentSystems.Domain.Transactions;
using static PaymentSystems.Domain.Accounts.AccountEvents;

namespace PaymentSystems.Domain.Accounts {
    public record AccountState : AggregateState<AccountState,AccountId> {
        // The amount available for your disposal
        public decimal AvailableBalance { get; set; }

        // This the balance shown in the statement as the start/end balance for the period
        // Also used for interest rate calculations
        public decimal AccountBalance { get; set; }

        public AccountTransactions PendingTransactions { get; init; } = new();

        public AccountTransactions BookedTransactions { get; init; } = new();

        private AccountState Handle(V1.TransactionInitiated e) {
            return this with
            {
                AvailableBalance = e.AvailableBalance,
                PendingTransactions = PendingTransactions.AddTransactions(
                    new AccountTransaction(new TransactionId(e.TransactionId), e.Amount)
                )
            };
        }

        internal AccountState Handle(V1.TransactionBooked e) {
            var initiated = PendingTransactions.FindTransaction(new TransactionId(e.TransactionId));

            return this with
            {
                AccountBalance = e.BookedBalance,
                PendingTransactions = PendingTransactions.RemoveTransaction(initiated),
                BookedTransactions = BookedTransactions.AddTransactions(initiated)
            };
        }
        
        internal AccountState Handle(V1.TransactionCancelled e) {
            var initiated = PendingTransactions.FindTransaction(new TransactionId(e.TransactionId));
            
            return this with
            {
                AvailableBalance = e.AvailableBalance,
                PendingTransactions = PendingTransactions.RemoveTransaction(initiated)
            };
        }

        public record AccountTransaction(TransactionId Id, decimal Amount);

        public class AccountTransactions : List<AccountTransaction> {
            public bool HasTransaction(TransactionId transactionId)
                => Exists(x => x.Id == transactionId);

            public AccountTransaction FindTransaction(TransactionId id)
                => Find(x => x.Id == id);

            public AccountTransactions AddTransactions(AccountTransaction transaction) {
                Add(transaction);
                return this;
            }

            public AccountTransactions RemoveTransaction(AccountTransaction transaction) {
                Remove(transaction);
                return this;
            }
        }
        public AccountState State { get; set; }
        public override AccountState When(object @event)
            => @event switch
            {
            V1.AccountOpened e =>
            new AccountState {
                Id               = new AccountId(e.AccountId),
                AvailableBalance = 10000,
                AccountBalance   = 10000
            },
            V1.TransactionInitiated e => State = State.Handle(e),
            V1.TransactionBooked e    => State = State.Handle(e),
            V1.TransactionCancelled e => State = State.Handle(e),
            _                      => State
        };
    }
}