using PaymentSystems.FrameWork;
using PaymentSystems.Domain;
using PaymentSystems.Contract;
using PaymentSystems.Domain.Payments;
using PaymentSystems.Domain.Accounts;
using System.Threading.Tasks;

namespace PaymentSystems.WebAPI.Application {
    public class PaymentCommandService : CommandService<Payment, PaymentId, PaymentState> {
        public PaymentCommandService(IAggregateStore store) : base(store) {
            OnNew<PaymentCommands.Submit>(
                (payment, cmd) =>
                {
                    payment.SubmitPayment(
                        new AccountId(cmd.AccountId),
                        new PaymentId(cmd.PaymentId),
                        new Payee(
                            cmd.payeeAccount?.Name,
                            new Domain.PayeeAccount(cmd.payeeAccount?.SortCode, cmd.payeeAccount?.AccountNumber)
                        ),
                        cmd.Amount
                    );
                }
            );

            OnExisting<PaymentCommands.Approve>(
                cmd => new PaymentId(cmd.PaymentId),
                (payment, cmd) =>
                    payment.ApprovePayment(cmd.ApprovedBy, cmd.ApprovedAt)
            );

            OnExisting<PaymentCommands.Execute>(
                cmd => new PaymentId(cmd.PaymentId),
                async (payment, cmd) =>
                {
                    var account = await Store.Load<Account, AccountId, AccountState>(new AccountId(cmd.AccountId), default);
                    payment.ExecutePayment(account);
                }
            );
        }

        public async Task<Payment> GetPaymentByAccountId(string accountId) => await Store.Load<Payment, PaymentId,PaymentState>(new AccountId(accountId), default);
    }
}