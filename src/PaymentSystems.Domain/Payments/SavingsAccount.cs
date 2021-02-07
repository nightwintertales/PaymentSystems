using System;
using PaymentSystems.Domain.Accounts;
using PaymentSystems.FrameWork;

namespace PaymentSystems.Domain.Payments
{
    public class SavingsAccount : Entity<AccountId>
    {
        public SavingsAccount(Action<object> applier) : base(applier)
        {
        }

        // Related events to Savings account
        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}