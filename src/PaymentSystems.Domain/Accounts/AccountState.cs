using System.Collections.Generic;
using PaymentSystems.FrameWork;
using PaymentSystems.Domain.Transactions;
using static PaymentSystems.Domain.Accounts.AccountEvents;

namespace PaymentSystems.Domain.Accounts {
    public record AccountState : AggregateState<AccountId> {
        //Typically it is the ending balance on the bank statement for each month - Not sure if this has to be in the aggregate. It looks like a field that is calculated on the fly in read model??
        public decimal AvailableBalance { get; set; }

        //Book balance is a banking term used to describe funds on deposit after adjustments
        public decimal BookedAmount { get; set; }

        public AccountTransactions InitiatedTransactions { get; init; } = new();

        public AccountTransactions BookedTransactions { get; init; } = new();

        internal AccountState Handle(V1.TransactionInitiated e) {
            return this with
            {
                AvailableBalance = e.AvailableBalance,
                InitiatedTransactions = InitiatedTransactions.AddTransactions(
                    new AccountTransaction(new TransactionId(e.TransactionId), e.Amount)
                )
            };
        }

        internal AccountState Handle(V1.TransactionBooked e) {
            var initiated = InitiatedTransactions.FindTransaction(new TransactionId(e.TransactionId));

            return this with
            {
                BookedAmount = e.BookedBalance,
                InitiatedTransactions = InitiatedTransactions.RemoveTransaction(initiated),
                BookedTransactions = BookedTransactions.AddTransactions(initiated)
            };
        }
        
        internal AccountState Handle(V1.TransactionCancelled e) {
            var initiated = InitiatedTransactions.FindTransaction(new TransactionId(e.TransactionId));
            
            return this with
            {
                AvailableBalance = e.AvailableBalance,
                InitiatedTransactions = InitiatedTransactions.RemoveTransaction(initiated)
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

    }
}