using System;
using Eventuous;
using PaymentSystems.Domain.Accounts;
using static PaymentSystems.Domain.Events.PaymentEvents.V1;
using static PaymentSystems.Domain.Shared.Shared;

namespace PaymentSystems.Domain.Payments {
    //Consider Payment Status that changes with different events
    public class Payment : Aggregate<PaymentState, PaymentId> {
       // public Payment() => State = new PaymentState();

        public void SubmitPayment(AccountId accountId, PaymentId paymentId, Payee payee, decimal amount) {
            Apply(
                new PaymentSubmitted()
                {
                    AccountId = accountId,
                    Amount = amount,
                   // PaymentId = paymentId.Value,
                    Payee = new V1.CounterParty(
                        payee.Name,
                        new V1.DomesticAccountDetails(payee.Account.SortCode, payee.Account.AccountNumber)
                    )
                }
                
            );
        }

        public void ApprovePayment(string approvedBy, DateTimeOffset approvedAt) {
            if (State.Status != PaymentStatus.Submitted) return;

            Apply(
                new PaymentApproved
                {
                 //   PaymentId = State.PaymentId,
                    ApprovedBy = approvedBy,
                    ApprovedAt = approvedAt
                }
            );
        }

        public void ExecutePayment(Account account) {
         //   if (State.Status == PaymentStatus.Executed) return;

            if (State.Status != PaymentStatus.Approved)
                throw new Exception("It's not approved");
            
            if (!account.CanExecutePayment(this))
                Apply(new PaymentDenied(State.PaymentId, "Not enough money", DateTime.UtcNow));
            else
                Apply(new PaymentExecuted {PaymentId = State.PaymentId});
        }
/*
        public override PaymentState When(object evt)
            => evt switch
            {
                PaymentSubmitted e =>
                    new PaymentState
                    {
                        AccountId = e.AccountId,
                        Amount = e.Amount,
                        Status = PaymentStatus.Submitted,
                        PaymentId = e.PaymentId
                    },
                PaymentApproved => State = State with {Status = PaymentStatus.Approved},
                PaymentExecuted => State = State with {Status = PaymentStatus.Executed},
                _ => State
            };
            
            */
    }
}