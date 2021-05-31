
/*
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
            // UpdateDocument - in Options set Upsert = true, Set id on insert
            throw new NotImplementedException();
        }

        public Task Consume(ConsumeContext<TransactionInitiated> context)
        {
            //MongoDB API TO SAVE UPDATE
            // 1. Query to find out if the transaction is already in the list.
            // 2. If found, ignore the event
            // 3. If not found, Update the available balance and insert the transaction to the list
            throw new NotImplementedException();
        }

        public Task Consume(ConsumeContext<TransactionBooked> context)
        {
            //MongoDB API TO SAVE UPDATE
            // UpdateDocument -> RemoveAll from transactions list where transaction id matches
            throw new NotImplementedException();
        }
        
        // Also, handle TransactionCancelled
        // 1. Update the balance to get the amount back
        // 2. Remove just like for Booked
    }
}

*/