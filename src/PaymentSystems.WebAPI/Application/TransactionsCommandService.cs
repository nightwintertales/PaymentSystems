using PaymentSystems.Contract;
using PaymentSystems.Domain.Transactions;
using Eventuous;
using PaymentSystems.FrameWork;

namespace PaymentSystems.WebAPI.Application
{
    //According to diagrams, TrasactionCommandService is responsible for
    //1. Initiating a transaction - Invoked by the Transactions Reactor
    //2. Booking a transaction - Invoked by the Integrations Reactor
    //3. Deniying a transaction   - Invoked By the Integrations Reactor
    public class TransactionsCommandService : CommandService<Transaction, TransactionId, TransactionState> 
    {
        public TransactionsCommandService(IAggregateStore store) : base(store) {
            OnNew<TransactionCommands.InitiateTransaction>(
                (transaction, cmd) =>
                {
                    transaction.InitiateTransaction(
                        new TransactionId(cmd.TransactionId),
                        new Domain.Accounts.AccountId(cmd.AccountId),
                        cmd.Amount,
                        initiatedAt: cmd.InitiatedAt
                    );
                }
            );

            OnExisting<TransactionCommands.BookTransaction>(
                cmd => new TransactionId(cmd.TransactionId),
                (transaction, cmd) =>
                    transaction.BookTransaction(cmd.BookedAt)
            );

            OnExisting<TransactionCommands.CancelTransaction>(
                cmd => new TransactionId(cmd.TransactionId),
                (transaction, cmd) =>
                    transaction.CancelTransaction(cmd.Reason , cmd.CancelledAt)
            );
        }
    }
}