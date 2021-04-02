using MassTransit;
using System;
using System.Threading.Tasks;
using static PaymentSystems.Domain.Accounts.AccountEvents.V1;

namespace PaymentSystems.WebAPI.Consumers
{
    public class AccountTransactionsConsumer : IConsumer<AccountOpened>, 
                                               IConsumer<TransactionInitiated>,
                                               IConsumer<TransactionBooked>
    {
        public Task Consume(ConsumeContext<AccountOpened> context)
        {
            //MongoDB API TO SAVE NEW
            throw new NotImplementedException();
        }

        public Task Consume(ConsumeContext<TransactionInitiated> context)
        {
            //MongoDB API TO SAVE UPDATE
            throw new NotImplementedException();
        }

        public Task Consume(ConsumeContext<TransactionBooked> context)
        {
            //MongoDB API TO SAVE UPDATE
            throw new NotImplementedException();
        }
    }
}
