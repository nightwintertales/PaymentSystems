using PaymentSystems.FrameWork;
using PaymentSystems.Domain;
using PaymentSystems.Contract;
using PaymentSystems.Domain.Payments;


namespace PaymentSystems.WebAPI.Application
{
    public class PaymentCommandService :  CommandService<Payment, PaymentId, PaymentState> {
        public PaymentCommandService(IAggregateStore store, IsRoomAvailable isRoomAvailable, ConvertCurrency convertCurrency) : base(store) {
            OnNew<PaymentCommands.Submit>(
                 (payment, cmd) =>
                    payment.SubmitPayment(new AccountId(), new PaymentId(), cmd.Amount));

            OnExisting<PaymentCommands.Approve>(
              (payment, cmd) =>
                    payment.ApprovePayment(cmd.Amount, new AccountId()));
            
                (payment, cmd) =>
            OnExisting<PaymentCommands.Execute>(
                    payment.ExecutePayment(cmd.Amount, new AccountId()));
        }
    }
}