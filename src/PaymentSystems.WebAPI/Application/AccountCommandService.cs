using Eventuous;
using PaymentSystems.Domain.Accounts;
using PaymentSystems.Domain.Transactions;
using PaymentSystems.FrameWork;
using static PaymentSystems.Contract.AccountCommands;

namespace PaymentSystems.WebAPI.Application {
    
    public class AccountCommandService : CommandService<Account, AccountId, AccountState> {

       
        public AccountCommandService(IAggregateStore store) : base(store) {
            
             OnNew<CreateAccount>(
                (account, cmd) =>
                account.OpenAccount(new AccountId(cmd.AccountId), cmd.CustomerId)
            );

            OnExisting<InitiateTransaction>(
                cmd => new AccountId(cmd.AccountId),
                (account, cmd) => account.InitiateTransaction(new TransactionId(cmd.TransactionId), cmd.Amount)
            );

            OnExisting<BookTransaction>(
                cmd => new AccountId(cmd.AccountId),
                (account, cmd) =>
                    account.BookTransaction(new TransactionId(cmd.TransactionId))
            );
            
            OnExisting<CancelTransaction>(
                cmd => new AccountId(cmd.AccountId),
                (account, cmd) =>
                    account.BookTransaction(new TransactionId(cmd.TransactionId))
            );
        }
    }
}