using PaymentSystems.FrameWork;
using PaymentSystems.Domain;
using PaymentSystems.Contract;
using PaymentSystems.Domain.Payments;
using PaymentSystems.Domain.Accounts;


namespace PaymentSystems.WebAPI.Application
{
    public class PaymentCommandService :  CommandService<Payment, PaymentId, PaymentState> {
        public PaymentCommandService(IAggregateStore store) : base(store) {
            OnNew<PaymentCommands.Submit>(
                 (payment, cmd) =>
                    payment.SubmitPayment(new AccountId(cmd.AccountId), new PaymentId(cmd.PaymentId), cmd.Amount));

            OnExisting<PaymentCommands.Approve>(
                 cmd => new PaymentId(cmd.PaymentId),
              (payment, cmd) =>
                    payment.ApprovePayment(cmd.Amount, new AccountId(cmd.AccountId)));
            
            OnExisting<PaymentCommands.Execute>(
                 cmd => new PaymentId(cmd.PaymentId),
                (payment, cmd) =>
                    payment.ExecutePayment(cmd.Amount, new AccountId(cmd.AccountId)));
        }
    }
}