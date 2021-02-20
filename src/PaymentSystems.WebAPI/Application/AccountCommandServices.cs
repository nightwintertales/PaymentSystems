using PaymentSystems.Contract;
using PaymentSystems.Domain.Accounts;
using PaymentSystems.Domain.Transactions;
using PaymentSystems.FrameWork;

namespace PaymentSystems.WebAPI.Application
{
    public class AccountCommandServices :  CommandService<Account, AccountId, AccountState> 
    {
        public AccountCommandServices(IAggregateStore store) : base(store) {
            OnExisting<AccountCommands.Initiate>(
                cmd => new AccountId(cmd.AccountId),
                 (account, cmd) => account.InitiateTransaction(new TransactionId(cmd.TransactionId), cmd.Amount));

            OnExisting<AccountCommands.Book>(
                 cmd => new AccountId(cmd.AccountId),
              (account, cmd) =>
                    account.BookTransaction(new TransactionId(cmd.TransactionId)));
            
        }
    }
}