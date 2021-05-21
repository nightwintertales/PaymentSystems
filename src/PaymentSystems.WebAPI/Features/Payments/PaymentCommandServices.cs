using Eventuous;
using PaymentSystems.Contract;
using PaymentSystems.Domain.Accounts;
using PaymentSystems.Domain.Payments;
using PaymentSystems.FrameWork;

namespace PaymentSystems.WebAPI.Features.Payments {
    public class PaymentCommandService : CommandService<Payment, PaymentId, PaymentState> {
        public PaymentCommandService(IAggregateStore store) : base(store) {
            OnNew<PaymentCommands.Submit>(
                (payment, cmd) =>
                {
                    payment.SubmitPayment(
                        new AccountId(cmd.AccountId),
                        new PaymentId(cmd.PaymentId),
                        new Domain.Payee(
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
                    var account = await Store.Load<Account>(new AccountId(cmd.AccountId), default);
                    payment.ExecutePayment();
                }
            );
        }
    }
}